using stock_manager_api.Dto;
using stock_manager_api.Models;

namespace stock_manager_api.Repository
{
    public class CarRepository : IRepository<ResponseCarDto, InsertCarDto>
    {
        private readonly StockManagerContext _context;
        public CarRepository(StockManagerContext context) { _context = context; }

        public IEnumerable<ResponseCarDto> GetAll()
        {
            var cars = _context.Cars;
            IEnumerable<ResponseCarDto> responseCarsDtos = 
                from car in cars
                select car.ToDto();

            return responseCarsDtos.OrderBy(car => car.id);
        }

        public ResponseCarDto GetById(int carId)
        {
            Car carById = _context.Cars.Find(carId) 
                ?? throw new KeyNotFoundException(NotFound(carId));

            return carById.ToDto();
        }

        public ResponseCarDto Add(InsertCarDto resource)
        {
            Car insertCar = new() { Plate = resource.Plate };

            _context.Cars.Add(insertCar);
            _context.SaveChanges();

            Car insertedCar = _context.Cars
                .OrderByDescending(car => car.CarId)
                .First();

            return insertedCar.ToDto();
        }

        public void Delete(int carId)
        {
            Car carById = _context.Cars.Find(carId) 
                ?? throw new KeyNotFoundException(NotFound(carId));

            _context.Remove(carById);
            _context.SaveChanges();
        }

        public ResponseCarDto Update(InsertCarDto resource, int carId)
        {
            Car carById = _context.Cars.Find(carId) 
                ?? throw new KeyNotFoundException(NotFound(carId));

            carById.Plate = resource.Plate;
            _context.Update(carById);
            _context.SaveChanges();

            return carById.ToDto();
        }

        public static string NotFound(int id)
        {
            return $"Car with id {id} was not Found";
        }
    }

}