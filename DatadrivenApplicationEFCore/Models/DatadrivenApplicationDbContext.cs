using Microsoft.EntityFrameworkCore;

namespace DatadrivenApplicationEFCore.Models
{
    public class DatadrivenApplicationDbContext : DbContext
    {
        public DatadrivenApplicationDbContext(DbContextOptions<DatadrivenApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Cake> Cakes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuration
            modelBuilder.ApplyConfigurationsFromAssembly(typeof
                (DatadrivenApplicationDbContext).Assembly);

            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Cake>().ToTable("Cakes");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails");

            //Fluent Api
            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired();
        }
    }
}
