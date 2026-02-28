using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Library;

public class DbContextProvider
{
    private readonly string _connectionString;
    
    public DbContextProvider(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<DbContext>()
            .UseSqlite(_connectionString)
            .Options;
        
        var context  = new DbContext(options);
        context.Database.EnsureCreated();
        return context;
    }
    
    public List<string> GetTableNames(DbContext context)
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