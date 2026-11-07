using System.Windows;
using System.Windows.Controls;
using PCL.Plugin.SDK;

namespace PCL.Lite.MarketTest;

public sealed class Plugin : IPclPlugin
{
    private IPluginContext? _context;

    public Task StartAsync(IPluginContext context, CancellationToken cancellationToken)
    {
        _context = context;
        context.Logger.Info("Marketplace test plugin started.");
        context.Ui.RegisterPage("market-test", "Marketplace Test", CreatePage);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _context?.Logger.Info("Marketplace test plugin stopped.");
        _context = null;
        return Task.CompletedTask;
    }

    private FrameworkElement CreatePage()
    {
        var context = _context ?? throw new InvalidOperationException("The plugin is not running.");
        var root = new StackPanel { Margin = new Thickness(24) };
        root.Children.Add(new TextBlock
        {
            Text = "PCL Lite Marketplace Test Plugin",
            FontSize = 24,
            FontWeight = FontWeights.SemiBold
        });
        root.Children.Add(new TextBlock
        {
            Text = $"Plugin API {context.Plugin.ApiVersion} · version {context.Plugin.Version}",
            Margin = new Thickness(0, 8, 0, 16),
            Opacity = 0.75
        });

        var result = new TextBlock { Text = "Use the button to test a plugin notification and private storage." };
        root.Children.Add(result);
        var button = new Button
        {
            Content = "Run plugin test",
            Padding = new Thickness(14, 8, 14, 8),
            Margin = new Thickness(0, 16, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left
        };
        button.Click += async (_, _) =>
        {
            try
            {
                var current = await context.Storage.ReadJsonAsync<int>("runs.json") + 1;
                await context.Storage.WriteJsonAsync("runs.json", current);
                context.Logger.Info($"Marketplace test action completed ({current}).");
                context.Ui.ShowNotification($"Plugin test completed. Saved run count: {current}.", PluginNotificationKind.Success);
                result.Text = $"Plugin test passed. This plugin's private storage records {current} runs.";
            }
            catch (Exception exception)
            {
                context.Logger.Error("Marketplace test action failed.", exception);
                context.Ui.ShowNotification("Plugin test failed. Check the launcher log.", PluginNotificationKind.Error);
                result.Text = "Plugin test failed. Check the launcher log.";
            }
        };
        root.Children.Add(button);
        return root;
    }
}
