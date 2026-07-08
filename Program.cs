using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.Json;

// whatismyip - prints public and internal (LAN) IPv4/IPv6 addresses.
// Cross-platform: Windows, macOS, Linux.

var noColor = args.Contains("--no-color") || Environment.GetEnvironmentVariable("NO_COLOR") is not null;
var jsonOutput = args.Contains("--json") || args.Contains("-j");

var publicIpTask = GetPublicIpAsync();
var internalAddresses = GetInternalAddresses();

string publicIp;
try
{
    publicIp = await publicIpTask;
}
catch (Exception ex)
{
    publicIp = $"unavailable ({ex.Message})";
}

if (jsonOutput)
{
    var payload = new
    {
        publicIp,
        internal_ = internalAddresses.Select(a => new { interfaceName = a.InterfaceName, address = a.Address, family = a.Family }),
    };
    // rename internal_ -> internal for cleaner JSON
    var json = JsonSerializer.Serialize(new Dictionary<string, object?>
    {
        ["publicIp"] = publicIp,
        ["internal"] = internalAddresses.Select(a => new { interfaceName = a.InterfaceName, address = a.Address, family = a.Family }),
    }, new JsonSerializerOptions { WriteIndented = true });
    Console.WriteLine(json);
    return;
}

WriteHeader("Public IP");
WriteLine($"  {publicIp}", ConsoleColor.Cyan);

Console.WriteLine();
WriteHeader("Internal IP(s)");
if (internalAddresses.Count == 0)
{
    WriteLine("  none found", ConsoleColor.DarkYellow);
}
else
{
    foreach (var addr in internalAddresses)
    {
        WriteLine($"  {addr.InterfaceName,-20} {addr.Family,-6} {addr.Address}", ConsoleColor.Green);
    }
}

void WriteHeader(string text)
{
    WriteLine(text, ConsoleColor.White, bold: true);
}

void WriteLine(string text, ConsoleColor color, bool bold = false)
{
    if (noColor)
    {
        Console.WriteLine(text);
        return;
    }

    Console.ForegroundColor = color;
    Console.WriteLine(text);
    Console.ResetColor();
}

static async Task<string> GetPublicIpAsync()
{
    // Try a few providers in order, falling back if one is down/blocked.
    string[] endpoints =
    {
        "https://api.ipify.org",
        "https://ifconfig.me/ip",
        "https://icanhazip.com",
    };

    using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };
    client.DefaultRequestHeaders.UserAgent.ParseAdd("whatismyip-cli/1.0");

    Exception? lastError = null;
    foreach (var url in endpoints)
    {
        try
        {
            var result = await client.GetStringAsync(url);
            return result.Trim();
        }
        catch (Exception ex)
        {
            lastError = ex;
        }
    }

    throw lastError ?? new Exception("all providers failed");
}

static List<InterfaceAddress> GetInternalAddresses()
{
    var results = new List<InterfaceAddress>();

    foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
    {
        if (nic.OperationalStatus != OperationalStatus.Up) continue;
        if (nic.NetworkInterfaceType is NetworkInterfaceType.Loopback or NetworkInterfaceType.Tunnel) continue;

        var props = nic.GetIPProperties();
        foreach (var ip in props.UnicastAddresses)
        {
            if (ip.Address.AddressFamily is not (AddressFamily.InterNetwork or AddressFamily.InterNetworkV6)) continue;
            if (IPAddress.IsLoopback(ip.Address)) continue;
            if (ip.Address.IsIPv6LinkLocal) continue;

            results.Add(new InterfaceAddress(
                nic.Name,
                ip.Address.AddressFamily == AddressFamily.InterNetwork ? "IPv4" : "IPv6",
                ip.Address.ToString()));
        }
    }

    return results
        .OrderBy(a => a.Family)
        .ThenBy(a => a.InterfaceName)
        .ToList();
}

record InterfaceAddress(string InterfaceName, string Family, string Address);
