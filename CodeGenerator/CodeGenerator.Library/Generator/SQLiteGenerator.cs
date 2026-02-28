using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Library;

public class SQLiteGenerator : IGenerator
{
    private Logger? _logger;
    private AppConfig? _config;
    private DbContext? _context;

    public void Initialize(AppConfig config, Logger logger)
    {
        _config = config;
        _logger = logger;
    }

    public bool Execute()
    {
        if (!Connect()) return false;

        return ReadAllTables();
    }

    private bool Connect()
    {
        if (_logger == null || _config == null)
            return false;

        try
        {
            var provider = new DbContextProvider(_config.ConnectionString);
            _context = provider.GetDbContext();

            _logger.Success($"SQLite connection with '{_config.ConnectionString}' successful");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"SQLite connection failed: {ex.Message}");
            return false;
        }
    }

    private bool ReadAllTables()
    {
        if (_logger == null || _context == null || _config == null)
            return false;

        try
        {
            using var connection = _context.Database.GetDbConnection();
            connection.Open();

            // Get tables
            var tables = new List<string>();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";
                using var reader = cmd.ExecuteReader();
                while (reader.Read()) tables.Add(reader.GetString(0));
            }

            if (tables.Count == 0)
            {
                _logger.Warning("No tables found.");
                return true;
            }

            string outputDir = _config.GeneratorOutputPath;
            Directory.CreateDirectory(outputDir);

           foreach (var table in tables)
                EntityGenerator.GenerateSQLiteEntity(connection, table, outputDir, _logger);

            _logger.Success("SQLite entity generation complete.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"Failed to read tables: {ex.Message}");
            return false;
        }
    }
}