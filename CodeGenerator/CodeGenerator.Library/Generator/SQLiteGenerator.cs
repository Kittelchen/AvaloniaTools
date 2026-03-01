using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Library;

public class SQLiteGenerator : IGenerator
{
    private readonly ILogger _logger;
    private readonly IConfig _config;
    private readonly IDbService _dbService;
    private readonly EntityGenerator _entityGenerator;
    
    private DbContext? _context;

    public SQLiteGenerator(ILogger logger,
        IConfig config,
        IDbService dbService,
        EntityGenerator entityGenerator)
    {
        _logger = logger;
        _config = config;
        _dbService = dbService;
        _entityGenerator = entityGenerator;
    }
    public void Initialize()
    {

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
            var provider = _dbService;
            
            _context = provider.GetDbContext(_config.ConnectionString);

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
                cmd.CommandText = Constants.GetSQLiteTables;
                _logger.Debug($"Running GetAllTables: {Constants.GetSQLiteTables}");
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
            {
                _entityGenerator.GenerateEntity(connection, table);
            }

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