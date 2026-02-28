using Microsoft.EntityFrameworkCore;
using Data.Model.Models;

namespace Data.Model;

public class AppDbContext : DbContext
{ 
    public DbSet<LogEntry> LogEntries { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
       
        
    }
}