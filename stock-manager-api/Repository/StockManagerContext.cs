using Microsoft.EntityFrameworkCore;
using stock_manager_api.Models;

namespace stock_manager_api.Repository
{
    public class StockManagerContext : DbContext, IStockManagerContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<AutoPart> AutoParts { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetedPart> BudgetedParts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string user = DotNetEnv.Env.GetString("USER", "person");
            string password = DotNetEnv.Env.GetString("PASSWORD", "secret");
            string database = DotNetEnv.Env.GetString("DATABASE", "stock_parts");
            string host = DotNetEnv.Env.GetString("HOST_ADDRESS", "localhost");
            string connectionString = $"Host={host}; Database={database}; Username={user}; Password={password}";
            optionsBuilder
                .UseNpgsql(connectionString);   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

    }
}
