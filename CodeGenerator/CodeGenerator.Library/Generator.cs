namespace CodeGenerator.Library;

public class Generator
{
    private Logger _logger;
    public bool Initialize(AppTypes type = AppTypes.CLI)
    {
        var config = AppConfig.Load("config.json");
        _logger = new (true, config.LogDirectory);
        _logger.Success($"Config file successfully loaded!");

        return true;
    }

    public bool Execute()
    {
        return false;
    }
    
}