using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using CodeGenerator.GUI.Services;
using CodeGenerator.GUI.ViewModels;
using CodeGenerator.GUI.Views;
using CodeGenerator.Library;
using CodeGenerator.Library.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeGenerator.GUI;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                CommonModules.AddCore(services);
                
                var dllLogger = new Logger();                   
                var uiLogger = new LoggerUiAdapter(dllLogger); 
                services.AddSingleton(uiLogger);              
                
                services.AddSingleton<ILogger>(provider => provider.GetRequiredService<LoggerUiAdapter>());
                
                services.AddSingleton<MainWindowViewModel>();
            })
            .Build();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            DisableAvaloniaDataAnnotationValidation();
            var mainWindowVm = host.Services.GetRequiredService<MainWindowViewModel>();
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowVm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}