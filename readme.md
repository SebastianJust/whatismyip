<div align="center">



\# 🌐 myip-cli



\*\*Your public and internal IP addresses, one command away.\*\*



\[!\[.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet\&logoColor=white)](https://dotnet.microsoft.com/download)

\[!\[Platform](https://img.shields.io/badge/platform-Windows%20%7C%20macOS%20%7C%20Linux-informational)](#)

\[!\[License](https://img.shields.io/badge/license-MIT-blue.svg)](#license)



```

$ whatismyip



Public IP

&#x20; 203.0.113.42



Internal IP(s)

&#x20; Ethernet             IPv4   192.168.1.23

&#x20; Wi-Fi                IPv4   192.168.1.24

&#x20; Ethernet             IPv6   fe80::1a2b:3c4d:5e6f:7g8h

```



</div>



\---



\## Why



Every OS makes you dig for this info differently — `ipconfig`, `ifconfig`, `ip addr`,

a browser tab to "what's my ip"... `whatismyip` gives you both your \*\*public\*\* and

\*\*internal\*\* addresses in one clean, colorized command, on any platform with .NET.



\## Features



\- 🔎 Public IP lookup with automatic fallback across three providers

\- 🖧 All internal (LAN) IPv4/IPv6 addresses, per network interface

\- 🎨 Colorized terminal output (or `--no-color` / `NO\_COLOR` for plain text)

\- 🤖 `--json` flag for scripting and piping into other tools

\- 🪶 Single self-contained command — no config, no API keys



\## Install



Requires the \[.NET 8 SDK or runtime](https://dotnet.microsoft.com/download).



```bash

dotnet tool install --global myip-cli

```



That's it — `whatismyip` is now available in any terminal.



Upgrade:



```bash

dotnet tool update --global myip-cli

```



Uninstall:



```bash

dotnet tool uninstall --global myip-cli

```



\## Usage



```bash

whatismyip                # colorized human-readable output

whatismyip --json         # machine-readable JSON

whatismyip -j             # short flag for --json

whatismyip --no-color     # disable ANSI colors

```



Example JSON output:



```json

{

&#x20; "publicIp": "203.0.113.42",

&#x20; "internal": \[

&#x20;   { "interfaceName": "Ethernet", "address": "192.168.1.23", "family": "IPv4" },

&#x20;   { "interfaceName": "Wi-Fi", "address": "192.168.1.24", "family": "IPv4" }

&#x20; ]

}

```



\## Build from source



```bash

git clone https://github.com/<your-username>/myip-cli.git

cd myip-cli

dotnet run -- --json

```



\## Publish your own build



```bash

dotnet pack -c Release

dotnet tool install --global --add-source ./nupkg myip-cli

```



To ship it to the world via NuGet.org:



```bash

dotnet pack -c Release

dotnet nuget push ./nupkg/myip-cli.1.0.0.nupkg \\

&#x20; --api-key YOUR\_NUGET\_API\_KEY \\

&#x20; --source https://api.nuget.org/v3/index.json

```



Anyone can then run `dotnet tool install --global myip-cli` — no server hosting required.



\## How it works



| Info | Source |

|---|---|

| Public IP | `api.ipify.org`, falling back to `ifconfig.me` and `icanhazip.com` |

| Internal IPs | `NetworkInterface.GetAllNetworkInterfaces()`, filtered to active, non-loopback, non-tunnel interfaces |



No native or platform-specific code — identical behavior on Windows, macOS, and Linux.



\## License



MIT — do whatever you want with it.



