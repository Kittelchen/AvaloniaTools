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
            var connection = _context.Database.GetDbConnection();
            connection.Open();

            var tables = new List<string>();

            // 1️⃣ Get all table names
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tables.Add(reader.GetString(0));
                }
            }

            if (tables.Count == 0)
            {
                _logger.Warning("No tables found in database.");
                connection.Close();
                return true;
            }

            _logger.Info("Tables and Columns:");

            // 2️⃣ For each table → get columns
            foreach (var table in tables)
            {
                _logger.Success($"Table: {table}");

                using var columnCommand = connection.CreateCommand();
                columnCommand.CommandText = $"PRAGMA table_info('{table}');";

                using var columnReader = columnCommand.ExecuteReader();

                while (columnReader.Read())
                {
                    string columnName = columnReader["name"]?.ToString() ?? "";
                    string columnType = columnReader["type"]?.ToString() ?? "";
                    string notNull = columnReader["notnull"]?.ToString() == "1" ? "NOT NULL" : "NULL";
                    string primaryKey = columnReader["pk"]?.ToString() == "1" ? "PK" : "";

                    _logger.Info($"   - {columnName} ({columnType}) {notNull} {primaryKey}");
                }
            }

            connection.Close();
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"Failed to read tables/columns: {ex.Message}");
            return false;
        }
    }
}