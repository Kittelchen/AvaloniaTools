using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Library;

public class Generator
{
    private Logger? _logger;
    private AppConfig _config = new();
    private DbContext? _context;

    public bool Initialize(AppTypes type = AppTypes.CLI)
    {
        _config = AppConfig.Load("C:\\temp\\config.json");
        _logger = new Logger(true, _config.LogDirectory);

        string dllVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";
        string exeVersion = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "unknown";

        _logger.Debug($"Library (DLL) version: {dllVersion}");
        _logger.Debug($"Executable (EXE) version: {exeVersion}");

        return true;
    }

    public bool Execute()
    {
        if (Connect())
        {
            ReadAllTables();
            return true;
        }

        return false;
    }

    private bool Connect()
    {
        if (_logger == null)
            throw new InvalidOperationException("Generator not initialized.");

        try
        {
            var provider = new DbContextProvider(_config.ConnectionString);
            _context = provider.GetDbContext(); // 🔥 store context

            _logger.Success($"Connection with '{_config.ConnectionString}' successful");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"Database connection failed: {ex.Message}");
            return false;
        }
    }

    private bool ReadAllTables()
    {
        if (_context == null || _logger == null)
            return false;

        try
        {
            var tableNames = new List<string>();

            var connection = _context.Database.GetDbConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText =
                "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                tableNames.Add(reader.GetString(0));
            }

            connection.Close();

            if (tableNames.Count == 0)
            {
                _logger.Warning("No tables found in database.");
            }
            else
            {
                _logger.Info("Tables found:");
                foreach (var table in tableNames)
                {
                    _logger.Info($" - {table}");
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"Failed to read tables: {ex.Message}");
            return false;
        }
    }
}