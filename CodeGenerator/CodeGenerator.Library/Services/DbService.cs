using Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Library;

public class DbService : IDbService
{
    private readonly ILogger _logger;
    private readonly IConfig  _config;
    
    private string _connectionString;
    
    public DbService(ILogger logger, IConfig config)
    {
        _logger = logger;
        _config = config;
    }
    
    public DbContext GetDbContext(string connectionString)
    {
        _connectionString = connectionString;
        var options = new DbContextOptionsBuilder<DbContext>()
            .UseSqlite(_connectionString)
            .Options;
        
        var context  = new DbContext(options);
        context.Database.EnsureCreated();
        return context;
    }
    
    public List<string> GetTableNames(DbContext context)
    {
        if (_config.DbType.IsEQ("sqlite"))
        {
            return GetSQLiteTableNames(context);
        }

        return null;
    }

    private List<string> GetSQLiteTableNames(DbContext context)
    {
        var tableNames = new List<string>();

        var connection = context.Database.GetDbConnection();
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

        return tableNames;
    }
}