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
            Budget budgetWithIdExist = _context.Budgets.Find(budgetId)
                ?? throw new KeyNotFoundException(BudgetNotFound(budgetId));
            
            Budget budgetById = _context.Budgets.Where(budget => budget.BudgetId == budgetId)
                .Include(budget => budget.Car)
                .Include(budget => budget.Client)                
                .Include(budget => budget.BudgetedParts)
                .ThenInclude(budgetedParts => budgetedParts.AutoPart)
                .First();
            
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

            ManageBudgeting(budget.Parts, autoparts, createBudget);

            _context.SaveChanges();

            return createBudget.ToDto();
        }

        public void Delete(int budgetId)
        {
            Budget budgetWithIdExist = _context.Budgets.Find(budgetId)
                ?? throw new KeyNotFoundException(BudgetNotFound(budgetId));
            
            Budget budgetById = _context.Budgets.Where(budget => budget.BudgetId == budgetId)
                .Include(budget => budget.BudgetedParts)
                .ThenInclude(budgetedParts => budgetedParts.AutoPart)
                .First();
            
            IEnumerable<BudgetedPart> budgeted = budgetById.BudgetedParts.Select(e => e);
            IEnumerable<AutoPart> stock = budgetById.BudgetedParts.Select(e => e.AutoPart);

            Unbudgeting(budgeted, stock);

            budgetById.BudgetedParts.Select(_context.BudgetedParts.Remove);

            _context.Remove(budgetById);
            _context.SaveChanges();
        }

        public ResponseBudgetDto Update(InsertBudgetDto budget, int budgetId)
        {
            throw new NotImplementedException();
        }

        public void ManageBudgeting(
            IEnumerable<AddAutoPartToBudgetDto> requestPart, IEnumerable<AutoPart> stockPart, Budget budget)
        {
            var checkStock = requestPart.Zip(stockPart, (need, have) => new { req = need, stock = have });

            foreach(var check in checkStock)
            {
                if (check.req.Quantity > check.stock.Stock)
                {
                    string message = $"autopart of id {check.stock.AutoPartId} {check.stock.Name} is out of stock for this budget";
                    throw new ArgumentException(message);
                }

                BudgetedPart partToBudget = new()
                {
                    AutoPart = check.stock,
                    Quantity = check.req.Quantity,
                    Budget = budget,
                };

                check.stock.Stock -= check.req.Quantity;
                check.stock.Budgeted += check.req.Quantity;
                _context.Update(check.stock);

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

        public static string BudgetNotFound(int id)
        {
            return $"Budget with id {id} was not found";
        }
    }
}