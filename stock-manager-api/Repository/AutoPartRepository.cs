using stock_manager_api.Dto;
using stock_manager_api.Models;

namespace stock_manager_api.Repository
{
    public class AutoPartRepository : IRepository<ResponseAutoPartDto, InsertAutoPartDto>
    {
        private readonly StockManagerContext _context;
        public AutoPartRepository(StockManagerContext context) { _context = context; }

        public IEnumerable<ResponseAutoPartDto> GetAll()
        {
            var autoParts = _context.AutoParts;
            IEnumerable<ResponseAutoPartDto> responseAutoPars = 
                from autoPart in autoParts
                select autoPart.ToDto();

            return responseAutoPars.OrderBy(autoPart => autoPart.id);
        }

        public ResponseAutoPartDto GetById(int autoPartId)
        {
            AutoPart autoPartById = _context.AutoParts.Find(autoPartId)
                ?? throw new KeyNotFoundException(NotFound(autoPartId));

            return autoPartById.ToDto();
        }
        public ResponseAutoPartDto Add(InsertAutoPartDto autoPart)
        {
            AutoPart insertAutoPart = new() 
            {
                Name = autoPart.Name,
                Stock = autoPart.Quantity,
                Budgeted = autoPart.Budgeted 
            };

            _context.Add(insertAutoPart);
            _context.SaveChanges();

            AutoPart insertedAutoPart = _context.AutoParts
                .OrderByDescending(autoPart => autoPart.AutoPartId)
                .First();

            return insertAutoPart.ToDto();
        }

        public void Delete(int autoPartId)
        {
            AutoPart toRemoveAutoPart = _context.AutoParts.Find(autoPartId)
                ?? throw new KeyNotFoundException(NotFound(autoPartId));
            
            _context.Remove(toRemoveAutoPart);
            _context.SaveChanges();
        }
        public ResponseAutoPartDto Update(InsertAutoPartDto resource, int autoPartId)
        {
            AutoPart toEditAutoPart = _context.AutoParts.Find(autoPartId)
                ?? throw new KeyNotFoundException(NotFound(autoPartId));

            toEditAutoPart.Name = resource.Name;

            _context.Update(toEditAutoPart);
            _context.SaveChanges();

            return toEditAutoPart.ToDto();
        }

        public static string NotFound(int id)
        {
            return $"Autopart with id {id} was not found";
        }
    }
}