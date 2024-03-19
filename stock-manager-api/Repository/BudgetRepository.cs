using Microsoft.EntityFrameworkCore;
using stock_manager_api.Dto;
using stock_manager_api.Models;
using stock_manager_api.Repository;

namespace stock_manager_api
{
    public class BudgetRepository
    {
        private readonly StockManagerContext _context;
        public BudgetRepository(StockManagerContext context) { _context = context; }

        public IEnumerable<ResponseBudgetDto> GetAll()
        {
            IEnumerable<Budget> budgets = _context.Budgets
                .Include(e => e.Car)
                .Include(e => e.Client)                
                .Include(e => e.BudgetedParts)
                .ThenInclude(e => e.AutoPart);
            
            IEnumerable<ResponseBudgetDto> budgetList = budgets.Select(budget => budget.ToDto());

            return budgetList;
        }

        public ResponseBudgetDto GetById(int budgetId)
        {
            Budget budgetById = GetBudgetById(budgetId);
            
            return budgetById.ToDto();
        }

        public ResponseBudgetDto Add(InsertBudgetDto budget)
        {
            Client client = _context.Clients.Find(budget.Client.id)
                ?? throw new KeyNotFoundException($"Client with id {budget.Client.id} was not found");
            Car car = _context.Cars.Find(budget.Car.id)
                ?? throw new KeyNotFoundException($"Car with id {budget.Car.id} was not found");

            IEnumerable<AutoPart> autoparts = budget.Parts
                .Select(part => _context.AutoParts.Find(part.Id)
                    ?? throw new KeyNotFoundException($"Autopart with id {part.Id} was not found!"));

            Budget createBudget = new() { Client = client, Car = car, BudgetedParts = [] };
            _context.Budgets.Add(createBudget);

            Budgeting(budget.Parts, autoparts, createBudget);

            _context.SaveChanges();

            return createBudget.ToDto();
        }

        public void Delete(int budgetId)
        {
            Budget budget = GetBudgetById(budgetId);
            
            IEnumerable<BudgetedPart> budgeted = budget.BudgetedParts.Select(e => e);
            IEnumerable<AutoPart> stock = budget.BudgetedParts.Select(e => e.AutoPart);

            Unbudgeting(budgeted, stock);

            budget.BudgetedParts.Select(_context.BudgetedParts.Remove);

            _context.Remove(budget);
            _context.SaveChanges();
        }

        public ResponseBudgetDto Update(InsertBudgetDto updatedBudget, int budgetId)
        {
            Budget budget = GetBudgetById(budgetId);

            IEnumerable<BudgetedPart> budgeted = budget.BudgetedParts.Select(e => e);
            IEnumerable<AutoPart> stock = budget.BudgetedParts.Select(e => e.AutoPart);
            
            Unbudgeting(budgeted, stock);

            budget.BudgetedParts.Select(_context.BudgetedParts.Remove);
            budget.BudgetedParts = [];
            _context.Update(budget);

            Budgeting(updatedBudget.Parts, stock, budget);

            _context.SaveChanges();

            return budget.ToDto();
        }

        public void Budgeting(IEnumerable<AddAutoPartToBudgetDto> requestParts, IEnumerable<AutoPart> inStockParts, Budget budget)
        {
            var compare = requestParts.Zip(inStockParts, (a, b) => new { requested = a, inStock = b });

            foreach(var comp in compare)
            {
                if (comp.requested.Quantity > comp.inStock.Stock)
                {
                    string message = $"autopart of id {comp.inStock.AutoPartId} {comp.inStock.Name} doesn't have enough stock for this budget";
                    throw new ArgumentException(message);
                }

                comp.inStock.Stock -= comp.requested.Quantity;
                comp.inStock.Budgeted += comp.requested.Quantity;
                _context.Update(comp.inStock);

                BudgetedPart partToBudget = new()
                {
                    AutoPart = comp.inStock,
                    Quantity = comp.requested.Quantity,
                    Budget = budget
                };
                _context.BudgetedParts.Add(partToBudget);
            }
        }
        public void Unbudgeting(IEnumerable<BudgetedPart> budgetedParts, IEnumerable<AutoPart> stockParts)
        {
            var unbugdeting = budgetedParts.Zip(stockParts, (remove, add) => new { budget = remove, stock = add });

            foreach (var remove in unbugdeting)
            {
                remove.stock.Budgeted -= remove.budget.Quantity;
                remove.stock.Stock += remove.budget.Quantity;
                _context.Update(remove.stock);
            }
        }

        public Budget GetBudgetById(int budgetId)
        {
            Budget budgetWithIdExists = _context.Budgets.Find(budgetId)
                ?? throw new KeyNotFoundException(BudgetNotFound(budgetId));
            
            Budget budgetById = _context.Budgets.Where(budget => budget.BudgetId == budgetId)
                .Include(budget => budget.Car)
                .Include(budget => budget.Client)                
                .Include(budget => budget.BudgetedParts)
                .ThenInclude(budgetedParts => budgetedParts.AutoPart)
                .First();

            return budgetById;
        }

        public static string BudgetNotFound(int id)
        {
            return $"Budget with id {id} was not found";
        }
    }
}