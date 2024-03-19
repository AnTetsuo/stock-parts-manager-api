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

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
            modelBuilder.Entity<AutoPart>().HasData
            (
                new AutoPart { AutoPartId = 1, Name = "BRACKET-ENGINE MOUNTING", Stock = 10, Budgeted = 0 },
                new AutoPart { AutoPartId = 2, Name = "INSULATOR ASSY-ENGINE MOUNTING", Stock = 10, Budgeted = 0 },
                new AutoPart { AutoPartId = 3, Name = "BLOCK ASSY-CYLINDER", Stock = 10, Budgeted = 0 },
                new AutoPart { AutoPartId = 4, Name = "SEAL-OIL LEVEL GAUGE GUIDE", Stock = 10, Budgeted = 0 },
                new AutoPart { AutoPartId = 5, Name = "COLLECTOR-INTAKE MANIFOLD", Stock = 10, Budgeted = 0 }
            );
            modelBuilder.Entity<Client>().HasData
            (
                new Client { ClientId = 1, Name = "Camila Tristão" },
                new Client { ClientId = 2, Name = "Teobaldo Albano" },
                new Client { ClientId = 3, Name = "Ivan Roval" },
                new Client { ClientId = 4, Name = "Fabricio Eliseu" },
                new Client { ClientId = 5, Name = "Arnaldo Reynaldo" }
            );
            modelBuilder.Entity<Car>().HasData 
            (
                new Car { CarId = 1, Plate = "KLV0553" },
                new Car { CarId = 2, Plate = "BLN4551" },
                new Car { CarId = 3, Plate = "LBT0505" },
                new Car { CarId = 4, Plate = "ASF6752" },
                new Car { CarId = 5, Plate = "JNQ7346" }
            );
        }

    }
}
