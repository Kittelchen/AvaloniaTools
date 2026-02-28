using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Library;

public class Generator
{
    protected Logger? _logger;
    protected AppConfig _config = new();
    protected DbContext? _context;

    public bool Initialize(string configPath)
    {
        _config = AppConfig.Load(configPath);
        _logger = new Logger(true, _config.LogDirectory);

        string dllVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";
        string exeVersion = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "unknown";

        _logger.Debug($"Library (DLL) version: {dllVersion}");
        _logger.Debug($"Executable (EXE) version: {exeVersion}");

        return true;
    }

    public bool Execute()
    {
        if (_logger == null)
            throw new InvalidOperationException("Generator not initialized.");

        IGenerator? dbGenerator = _config.DbType.ToLower() switch
        {
            "sqlite" => new SQLiteGenerator(),
            _        => null
        };

        if (dbGenerator == null)
        {
            _logger.Error($"Unsupported DbType '{_config.DbType}' in config.");
            return false;
        }

        // Pass the config and logger to the subclass
        dbGenerator.Initialize(_config, _logger);

        return dbGenerator.Execute();
    }
}