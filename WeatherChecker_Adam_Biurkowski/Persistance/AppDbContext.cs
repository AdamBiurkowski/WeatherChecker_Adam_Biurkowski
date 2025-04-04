using Microsoft.EntityFrameworkCore;
using WeatherChecker_Adam_Biurkowski.Models;

namespace WeatherChecker_Adam_Biurkowski.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Account> Accounts => Set<Account>();
    }
}
