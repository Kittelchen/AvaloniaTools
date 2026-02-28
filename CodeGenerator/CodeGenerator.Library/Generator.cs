namespace CodeGenerator.Library;

public class Generator
{
    private Logger _logger = new Logger(true);
    public bool SetUp(AppTypes type = AppTypes.CLI)
    {
        if (type == AppTypes.CLI)
        {
            _logger.Info("Using CLI");
            _logger.Success("Setup successful!");
            return true;
        }
        _logger.Error("Setup failed!");
        return false;
    }

    public bool Execute()
    {
        return false;
    }
    
}