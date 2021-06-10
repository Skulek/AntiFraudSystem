using Microsoft.EntityFrameworkCore;

namespace AntiFraud.Orders
{
    public class AntiFraudDbContext: DbContext
    {
        public AntiFraudDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Entities.Order> Orders { get; set; }
        public DbSet<Products.Entities.Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Order>().HasMany(x=>x.Products);
            modelBuilder.Entity<Entities.Order>().HasOne(x => x.Address);

            base.OnModelCreating(modelBuilder);
        }
    }
}
