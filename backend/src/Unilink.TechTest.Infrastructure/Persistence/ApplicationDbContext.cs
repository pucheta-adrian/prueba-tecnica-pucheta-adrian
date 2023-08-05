using System.Data.Entity;
using Unilink.TechTest.Domain.Entities;

namespace Unilink.TechTest.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext() : base("RouletteDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Migrations.Configuration>());
            
        }

        public DbSet<Balance> Balances { get; set; }
    }
}