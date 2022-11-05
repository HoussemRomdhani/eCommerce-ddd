using eCommerce.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure;

public class CatalogContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public CatalogContext(DbContextOptions<CatalogContext> options)
   : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(x =>
        {
            x.ToTable("Products").HasKey(k => k.Id);
            x.Property(p => p.Id).HasColumnName("Id");
            x.Property(p => p.Name);
            x.Property(p => p.Created);
            x.Property(p => p.Modified);
            x.Property(p => p.Active);
            x.Property(p => p.Quantity);
            x.Property(p => p.Cost);
            x.HasOne(p => p.Category).WithOne().HasForeignKey<Category>(c => c.Id);
        });

        modelBuilder.Entity<Category>(x =>
            {
                x.ToTable("Categories").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("Id");
                x.Property(p => p.Name);
            });
    }
}
