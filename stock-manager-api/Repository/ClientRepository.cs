using stock_manager_api.Dto;
using stock_manager_api.Models;

namespace stock_manager_api.Repository
{
    public class ClientRepository : IRepository<ResponseClientDto, InsertClientDto>
    {
        protected readonly StockManagerContext _context;

        public ClientRepository(StockManagerContext context) { _context = context; }

        public IEnumerable<ResponseClientDto> GetAll()
        {
            var clients = _context.Clients;

            IEnumerable<ResponseClientDto> clientList = 
                from client in clients
                select client.ToDto();
            
            return clientList.OrderBy(client => client.id);
        }

        public ResponseClientDto GetById(int clientId)
        {
            Client clientById = _context.Clients.Find(clientId)
                ?? throw new KeyNotFoundException(NotFound(clientId));

            return clientById.ToDto();
        }

        public ResponseClientDto Add(InsertClientDto resource)
        {
            Client createClient = new() { Name = resource.Name };

            _context.Clients.Add(createClient);
            _context.SaveChanges();

            Client insertedClient = _context.Clients
                .OrderByDescending(client => client.ClientId)
                .First();
            
            return insertedClient.ToDto();
        }

        public void Delete(int clientId)
        {
            Client clientById = _context.Clients.Find(clientId)
                ?? throw new KeyNotFoundException(NotFound(clientId));
            
            _context.Clients.Remove(clientById);
            _context.SaveChanges();
        }

        public ResponseClientDto Update(InsertClientDto resource, int clientId)
        {
            Client clientById = _context.Clients.Find(clientId)
                ?? throw new KeyNotFoundException(NotFound(clientId));
            
            clientById.Name = resource.Name;
            _context.Update(clientById);
            _context.SaveChanges();

            return clientById.ToDto();
        }

        public static string NotFound(int clientId) { return $"Client with id {clientId} was not found"; }
    }
}