using Microsoft.EntityFrameworkCore;

namespace CodeGenerator.Library;

public interface IDbService
{
    DbContext GetDbContext(string connectionString);
    
    List<string> GetTableNames(DbContext context);
}