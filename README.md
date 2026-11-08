# PCL Lite Marketplace Test Plugin

This signed v1.1.1 test plugin exposes a page with button-driven probes for the PCL Lite plugin API: lifecycle and logging, metadata, storage and settings (including bytes and streams), themed UI controls and dialogs, page lifecycle and removal, typed events, HTTP requests with case-insensitive echoed-header checks and timeouts, binary responses, downloads with SHA-256 verification, services, launcher information, and Minecraft instance/configuration queries.

The “run all” button skips dialogs, notifications, game launches, and opening external URLs or paths. It still needs network access for the HTTP and download probes; network failures are reported as failed checks. Actions that launch a game or open a URL/path require a separate explicit button click, and game launches ask for confirmation.

HTTP echo checks parse header names case-insensitively, matching HTTP semantics; diagnostics include the actual echoed header name, value, and a short response snippet.

The plugin runs inside the launcher process with the launcher's user permissions. Its self-signed certificate verifies package integrity, not author identity or isolation. Version 1.1.1 reuses the v1.1.0 test signing certificate (thumbprint `8FE24FA6A6D0D8C675DD8B168DF52175F790884D`); the signer remains unchanged from v1.1.0.

## Build

Install the .NET 10 SDK and check out the PCL Lite source repository next to this repository. Build with the SDK project path supplied explicitly:

```powershell
dotnet build .\PCL.Lite.MarketTest.csproj -c Release -p:PclLiteSdkProject="..\PCL-CE\PCL.Plugin.SDK\PCL.Plugin.SDK.csproj"
```

Package the clean Release staging directory with `PCL.Plugin.Tools`. Keep the signing PFX and password out of source control and the `.pclplugin` package.

## v1.1.1

Fixes a false failure in the GET custom-header probe: HTTP header names are case-insensitive and echo services may normalize their casing. GET and POST header tests now parse echoed JSON case-insensitively, report the actual echoed header and a short response excerpt on failure, and the full-test button ignores duplicate clicks while a run is active.
