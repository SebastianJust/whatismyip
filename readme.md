<div align="center">

# 🌐 whatismyip

### Public and local IP addresses from your terminal — fast, clean, scriptable.

<br />

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet\&logoColor=white)](https://dotnet.microsoft.com/download)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
[![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20macOS%20%7C%20Linux-informational)](#)

**One command. No browser. No ads. No platform-specific commands.**

</div>

---

## Why?

Checking your IP should be simple.

On Windows you use `ipconfig`.
On Linux you use `ip addr`.
On macOS you might use `ifconfig`.
For your public IP, you usually open a browser.

`whatismyip` gives you both your **public IP** and **local/internal IP addresses** in one predictable command.

```bash
whatismyip
```

> [!TIP]
> Use `--json` when you want to pipe the result into scripts, CI jobs, logs, or automation tools.

---

## Quick start

Install globally using the .NET CLI:

```bash
dotnet tool install --global whatismyip
```

Then run:

```bash
whatismyip
```

Example output:

```txt
Public IP
  203.0.113.42

Internal IP(s)
  Ethernet             IPv4   192.168.1.23
  Wi-Fi                IPv4   192.168.1.24
  Ethernet             IPv6   fe80::1a2b:3c4d:5e6f:7a8b
```

> [!IMPORTANT]
> Requires the [.NET 8 SDK or runtime](https://dotnet.microsoft.com/download).

---

## CLI usage

```bash
whatismyip [options]
```

| Option       | Alias | Description                  |
| ------------ | ----: | ---------------------------- |
| `--json`     |  `-j` | Output machine-readable JSON |
| `--no-color` |       | Disable ANSI colors          |
| `--help`     |  `-h` | Show help                    |
| `--version`  |       | Show version                 |

> [!NOTE]
> Colors are enabled by default when running in an interactive terminal.

---

## Examples

### Human-readable output

```bash
whatismyip
```

```txt
Public IP
  203.0.113.42

Internal IP(s)
  Ethernet             IPv4   192.168.1.23
  Wi-Fi                IPv4   192.168.1.24
```

---

### JSON output

```bash
whatismyip --json
```

```json
{
  "publicIp": "203.0.113.42",
  "internal": [
    {
      "interfaceName": "Ethernet",
      "address": "192.168.1.23",
      "family": "IPv4"
    },
    {
      "interfaceName": "Wi-Fi",
      "address": "192.168.1.24",
      "family": "IPv4"
    }
  ]
}
```

---

### Use in PowerShell

```powershell
$result = whatismyip --json | ConvertFrom-Json

$result.publicIp
$result.internal
```

---

### Use in Bash

```bash
whatismyip --json | jq -r '.publicIp'
```

---

### Disable colors

```bash
whatismyip --no-color
```

Or use the standard environment variable:

```bash
NO_COLOR=1 whatismyip
```

> [!IMPORTANT]
> `--no-color` is useful when writing output to files, logs, CI systems, or tools that do not support ANSI escape codes.

---

## Features

| Feature               | Description                                       |
| --------------------- | ------------------------------------------------- |
| 🌍 Public IP lookup   | Fetches your public IP from external providers    |
| 🔁 Provider fallback  | Automatically tries another provider if one fails |
| 🖧 Local IP detection | Lists active local network interfaces             |
| 🔢 IPv4 and IPv6      | Shows both address families                       |
| 📦 JSON output        | Works well with scripts and automation            |
| 🧘 No config          | No API keys, accounts, or setup                   |
| 🖥️ Cross-platform    | Windows, macOS, and Linux                         |
| 🛡️ Pure .NET         | No native dependencies                            |

---

## Installation

### Install from NuGet.org

```bash
dotnet tool install --global whatismyip
```

Upgrade:

```bash
dotnet tool update --global whatismyip
```

Uninstall:

```bash
dotnet tool uninstall --global whatismyip
```

---

## Install from local `.nupkg`

This is useful if you want to install the tool directly from the pre-built package in this repository without publishing it to NuGet.org.

Works on:

* Windows
* macOS
* Linux

> [!IMPORTANT]
> You need the [.NET 8 SDK](https://dotnet.microsoft.com/download), not just the runtime.
> Installing .NET tools from a local `.nupkg` package source requires the SDK.

---

### 1. Download the package

`dotnet tool install` installs packages from a folder source, not directly from a `.nupkg` URL.

So first, download the package into a local folder.

#### Windows PowerShell

```powershell
mkdir $env:USERPROFILE\nuget-local -Force

Invoke-WebRequest `
  -Uri "https://github.com/SebastianJust/whatismyip/raw/refs/heads/main/nupkg/whatismyip.1.0.0.nupkg" `
  -OutFile "$env:USERPROFILE\nuget-local\whatismyip.1.0.0.nupkg"
```

#### macOS / Linux

```bash
mkdir -p ~/nuget-local

curl -L "https://github.com/SebastianJust/whatismyip/raw/refs/heads/main/nupkg/whatismyip.1.0.0.nupkg" \
  -o ~/nuget-local/whatismyip.1.0.0.nupkg
```

> [!NOTE]
> The folder name `nuget-local` is just a local package source.
> You can use another folder if you prefer.

---

### 2. Install as a global .NET tool

Install the package from the local folder.

#### Windows PowerShell

```powershell
dotnet tool install --global --add-source "$env:USERPROFILE\nuget-local" whatismyip
```

#### macOS / Linux

```bash
dotnet tool install --global --add-source ~/nuget-local whatismyip
```

---

### 3. Verify the installation

Run:

```bash
whatismyip
```

Example output:

```txt
Public IP
  203.0.113.42

Internal IP(s)
  Wi-Fi                IPv4   192.168.1.24
  Ethernet             IPv6   fe80::1a2b:3c4d:5e6f:7a8b
```

---

## Troubleshooting

### `whatismyip` is not recognized

If the command is not found, your global .NET tools folder may not be on your `PATH`.

Add this folder to your `PATH`:

| OS            | Tools folder                  |
| ------------- | ----------------------------- |
| Windows       | `%USERPROFILE%\.dotnet\tools` |
| macOS / Linux | `~/.dotnet/tools`             |

After updating your `PATH`, close and reopen your terminal.

> [!TIP]
> You can check your installed global tools with:
>
> ```bash
> dotnet tool list --global
> ```

---

## Updating from a local `.nupkg`

Download the newer `.nupkg` into the same local folder, then run:

### Windows PowerShell

```powershell
dotnet tool update --global --add-source "$env:USERPROFILE\nuget-local" whatismyip
```

### macOS / Linux

```bash
dotnet tool update --global --add-source ~/nuget-local whatismyip
```

---

## How it works

`whatismyip` collects two types of IP addresses:

| Type      | Source                                            |
| --------- | ------------------------------------------------- |
| Public IP | External public IP providers                      |
| Local IPs | `.NET NetworkInterface.GetAllNetworkInterfaces()` |

Public IP providers are tried in order:

```txt
api.ipify.org
ifconfig.me
icanhazip.com
```

Local interfaces are filtered to remove noisy or irrelevant entries such as:

```txt
loopback
inactive interfaces
tunnel interfaces
```

This keeps the output focused on the addresses you are most likely to care about.

---

## JSON contract

When using `--json`, the output follows this shape:

```ts
type WhatIsMyIpResult = {
  publicIp: string | null;
  internal: InternalIpAddress[];
};

type InternalIpAddress = {
  interfaceName: string;
  address: string;
  family: "IPv4" | "IPv6";
};
```

> [!TIP]
> The JSON output is intended to be stable so it can safely be used in scripts.

---

## Build from source

Clone the repository:

```bash
git clone https://github.com/SebastianJust/whatismyip.git
cd whatismyip
```

Run locally:

```bash
dotnet run
```

Run with arguments:

```bash
dotnet run -- --json
```

Run tests:

```bash
dotnet test
```

---

## Package locally

Create a NuGet package:

```bash
dotnet pack -c Release
```

Install the tool locally from the generated package:

```bash
dotnet tool install --global --add-source ./nupkg whatismyip
```

---

## Publish to NuGet.org

Create a release package:

```bash
dotnet pack -c Release
```

Publish the package:

```bash
dotnet nuget push ./nupkg/whatismyip.1.0.0.nupkg \
  --api-key YOUR_NUGET_API_KEY \
  --source https://api.nuget.org/v3/index.json
```

After publishing, users can install it with:

```bash
dotnet tool install --global whatismyip
```

> [!WARNING]
> Never commit your NuGet API key to source control.

---

## Roadmap ideas

* `--public-only`
* `--local-only`
* `--ipv4`
* `--ipv6`
* `--copy`
* Provider timeout configuration
* Custom public IP provider support

---

## Contributing

Pull requests are welcome.

For small fixes, feel free to open a PR directly.
For larger changes, please open an issue first so the approach can be discussed.

```bash
git checkout -b feature/my-change
dotnet test
```

---

## License

Released under the [MIT License](LICENSE).

---

<div align="center">

Built with .NET, coffee, and `System.Net.NetworkInformation`.

</div>
