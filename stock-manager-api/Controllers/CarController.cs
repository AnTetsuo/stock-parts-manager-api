using Microsoft.AspNetCore.Mvc;
using stock_manager_api.Dto;
using stock_manager_api.Repository;

namespace stock_manager_api.Controllers
{
    [ApiController]
    [Route("cars")]
    public class CarController : Controller 
    {
        private readonly CarRepository carRepository;
        public CarController(CarRepository repository) { carRepository = repository; }

        [HttpGet(Name = "GetCars")]
        public IActionResult GetCars() 
        {
            try 
            {
                return Ok(carRepository.GetAll());        
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error"});
            }
        }

        [HttpGet("{carId}")]
        public IActionResult GetCarById(int carId)
        {
            try
            {
                ResponseCarDto carById = carRepository.GetById(carId);
                
                return Ok(carById);
            } 
            catch (KeyNotFoundException NotFoundEx)
            {
                return NotFound(NotFoundEx.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpPost]
        public IActionResult PostCar([FromBody] InsertCarDto car)
        {
            try
            {
                ResponseCarDto createdCar = carRepository.Add(car);

                return CreatedAtRoute("GetCars", createdCar);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpPut("{carId}")]
        public IActionResult PutCar(int carId, [FromBody] InsertCarDto car)
        {
            try
            {
                ResponseCarDto carToEdit = carRepository.Update(car, carId);

                return CreatedAtRoute("GetCars", carToEdit);
            }
            catch(KeyNotFoundException NotFoundEx)
            {
                return NotFound(NotFoundEx.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpDelete("{carId}")]
        public IActionResult DeleteCar(int carId)
        {
            try
            {
                carRepository.Delete(carId);

                return NoContent();
            }
            catch (KeyNotFoundException NotFoundEx)
            {
                return NotFound(NotFoundEx.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
    }
}