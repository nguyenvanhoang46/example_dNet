using example.Entities;
using Microsoft.EntityFrameworkCore;

namespace example.Data;

public class MyDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public MyDbContext(DbContextOptions options) : base(options)
    {
    }
    
    
}