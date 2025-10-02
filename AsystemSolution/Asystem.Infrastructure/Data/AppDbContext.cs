using Asystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Asystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderTask> OrderTasks => Set<OrderTask>();
        public DbSet<Material> Materials => Set<Material>();
        public DbSet<Formula> Formulas => Set<Formula>();
    public DbSet<Asystem.Core.Entities.Product> Products => Set<Asystem.Core.Entities.Product>();


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<OrderTask>().HasKey(t => t.Id);
            modelBuilder.Entity<Material>().HasKey(m => m.Id);
            modelBuilder.Entity<Formula>().HasKey(f => f.Id);
            modelBuilder.Entity<Asystem.Core.Entities.Product>().HasKey(p => p.Id);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Tasks)
                .WithOne(t => t.Order!)
                .HasForeignKey(t => t.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
