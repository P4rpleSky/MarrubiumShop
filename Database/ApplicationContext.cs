using MarrubiumShop.Database.Entitites;
using Microsoft.EntityFrameworkCore;

namespace MarrubiumShop.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=marrubium;Username=postgres;Password=123");
        }
    }
}
