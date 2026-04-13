using Microsoft.EntityFrameworkCore;
using System.IO;

namespace SteganographyLSB.Models;

public class HistoryContext : DbContext
{
    public DbSet<OperationRecord> Operations { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Save database in the local application data folder
        options.UseSqlite($"Data Source=history.db");
    }
}
