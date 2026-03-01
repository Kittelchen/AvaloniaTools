using System.Threading.Tasks;
using System.Windows.Input;
using CodeGenerator.GUI.Services;
using CodeGenerator.Library;
using CommunityToolkit.Mvvm.Input;

namespace CodeGenerator.GUI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly Generator _generator;
    public LoggerUiAdapter Logger { get; }
    public string ButtonGenerateText => "Generate!";
    
    public MainWindowViewModel(Generator generator,  LoggerUiAdapter logger)
    {
        _generator = generator;
        Logger = logger;
        
        Logger.Info("Ready to generate!");
    }
    
    [RelayCommand]
    private async Task Generate()
    {
        if (await Initialize())
        {
            await Task.Run(() => _generator.Execute());
        }
    }

    private async Task<bool> Initialize()
    {
        bool result = await Task.Run(() => _generator.Initialize(@".\config.json"));
        return result;
    }
}