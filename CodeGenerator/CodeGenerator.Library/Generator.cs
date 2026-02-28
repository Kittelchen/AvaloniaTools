using System.Reflection;

namespace CodeGenerator.Library;

public class Generator
{
    private Logger _logger;
    public bool Initialize(AppTypes type = AppTypes.CLI)
    {
        // load config file
        var config = AppConfig.Load("config.json");
                
        _logger = new (true, config.LogDirectory);
        
        // load version number
        string dllVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";
        string exeVersion = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "unknown";
        
        _logger.Debug($"Library (DLL) version: {dllVersion}");
        _logger.Debug($"Executable (EXE) version: {exeVersion}");

        _logger.Success($"Config file successfully loaded!");

        return true;
    }

    public bool Execute()
    {
        return false;
    }
    
}