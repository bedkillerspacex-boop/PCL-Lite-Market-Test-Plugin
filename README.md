# PCL Lite Marketplace Test Plugin

This signed test plugin exposes a page with button-driven probes for the PCL Lite plugin API: lifecycle and logging, metadata, storage and settings (including bytes and streams), themed UI controls and dialogs, page lifecycle and removal, typed events, HTTP requests with headers and timeouts, binary responses, downloads with SHA-256 verification, services, launcher information, and Minecraft instance/configuration queries.

The “run all” button skips dialogs, notifications, game launches, and opening external URLs or paths. It still needs network access for the HTTP and download probes; network failures are reported as failed checks. Actions that launch a game or open a URL/path require a separate explicit button click, and game launches ask for confirmation.

The plugin runs inside the launcher process with the launcher's user permissions. Its self-signed certificate verifies package integrity, not author identity or isolation. The v1.1.0 package uses a new test signing certificate because the private key for v1.0.0 is unavailable; check the displayed signer fingerprint when updating.

## Build

Install the .NET 10 SDK and check out the PCL Lite source repository next to this repository. Build with the SDK project path supplied explicitly:

```powershell
dotnet build .\PCL.Lite.MarketTest.csproj -c Release -p:PclLiteSdkProject="..\PCL-CE\PCL.Plugin.SDK\PCL.Plugin.SDK.csproj"
```

Package the clean Release staging directory with `PCL.Plugin.Tools`. Keep the signing PFX and password out of source control and the `.pclplugin` package.
