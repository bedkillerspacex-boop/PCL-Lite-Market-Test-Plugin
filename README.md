# PCL Lite Marketplace Test Plugin

A small plugin published to exercise the PCL Lite plugin marketplace install flow. Its page shows the plugin API and version, and its test button writes a counter into the plugin's own storage and displays a notification.

This plugin runs in the launcher process with the launcher's user permissions. Its signature proves that the package has not changed since it was signed; it does not prove the author's identity or isolate the plugin.

## Build

Install the .NET 10 SDK and check out the PCL Lite source repository next to this repository. Then build with the SDK project path supplied explicitly:

```powershell
dotnet build .\PCL.Lite.MarketTest.csproj -c Release -p:PclLiteSdkProject="..\PCL-CE\PCL.Plugin.SDK\PCL.Plugin.SDK.csproj"
```

The release package is built from the output directory with the PCL Plugin Tools. The signing certificate's private key is kept by the publisher and is not included in this repository or the package.
