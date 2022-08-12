using FinanceTrackingBot.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackingBot.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; } = null!;
    }
}
