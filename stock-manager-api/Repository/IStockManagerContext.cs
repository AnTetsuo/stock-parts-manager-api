using Microsoft.EntityFrameworkCore;
using stock_manager_api.Models;

namespace stock_manager_api.Repository
{
    public interface IStockManagerContext 
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<AutoPart> AutoParts { get; set; }
        public DbSet<BudgetedPart> BudgetedParts { get; set; }
        public int SaveChanges();
    }
}