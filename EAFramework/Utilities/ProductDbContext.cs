using Microsoft.EntityFrameworkCore;

namespace EAFramework.Utilities;

public class ProductDbContext : DbContext
{
    public DbSet<DbHelperForSqlite.Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=/Users/karthik/Downloads/EAApp_LocalMachine-6/ProductAPI/Product.db");
    }
}