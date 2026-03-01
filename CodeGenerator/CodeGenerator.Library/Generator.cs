using Common.Extensions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Library;

public class Generator
{
    private readonly ILogger _logger;
    private readonly IConfig _config;
    private readonly GeneratorFactory _factory;
    //protected AppConfig _config = new();
    protected DbContext? _context;

    public Generator(ILogger logger, GeneratorFactory factory, IConfig config)
    {
        _logger = logger;
        _config = config;
        _factory = factory;
        
    }
    public bool Initialize(string configPath)
    {
        string dllVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";
        string exeVersion = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "unknown";

        _logger.Debug($"Library (DLL) version: {dllVersion}");
        _logger.Debug($"Executable (EXE) version: {exeVersion}");
        
        return true;
    }

    public bool Execute()
    {
        try
        {
            if (_logger is null)
                throw new InvalidOperationException("Generator not initialized.");

            if (_config.GeneratorOutputPath.IsNullOrEmpty())
            {
                _logger.Error("GeneratorOutputPath not set.");
            }

            if (_config.DbType.IsNullOrEmpty())
            {
                _logger.Error("DbType not set.");
            }

            var dbGenerator = _factory.Create(_config.DbType);

            if (dbGenerator is null)
            {
                _logger.Error($"Unsupported DbType '{_config.DbType}' in config.");
                return false;
            }

            return dbGenerator.Execute();
        }
        catch (Exception ex)
        {
            _logger.Error($"Exception: {ex.Message}");
            throw;
        }
    }
}