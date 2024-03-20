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

        /// <summary>
        /// Lista todas as placas de carro registradas
        /// </summary>
        /// <response code="200">Retorna a lista no corpo da requisição</response>
        /// <response code="500">Informa sobre erro interno do serviço</response> 
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


        /// <summary>
        /// Busca um carro pelo seu id
        /// </summary>
        /// <param name="carId">O id do carro a buscar</param>
        /// <response code="200">Retorna no corpo da response o id e placa do carro</response>
        /// <response code="400">Erro de validação da payload</response>
        /// <response code="404">Id do carro não encontrado</response>
        /// <response code="500">Informa sobre erro interno do serviço</response> 
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

        /// <summary>
        /// Cadastra a placa de um carro
        /// </summary>
        /// <param name="car">Um objeto com a placa do carro</param>
        /// <response code="201">Retorna o id e placa registrados</response>
        /// <response code="400">Erro de validação da payload</response>
        /// <response code="500">Informa sobre erro interno do serviço</response> 
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

        /// <summary>
        /// Edita a placa de um carro registrado
        /// </summary>
        /// <param name="carId">O id do carro a ser editado</param>
        /// <param name="car">A payload com a nova placa</param>
        /// <response code="201">Retorna o Id e a nova placa registrada</response>
        /// <response code="400">Erro de validação da payload</response>
        /// <response code="404">Id do carro não encontrado</response>
        /// <response code="500">Informa sobre erro interno do serviço</response> 
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

        /// <summary>
        /// Remove um carro registrado
        /// </summary>
        /// <param name="carId">O id do carro a ser removido</param>
        /// <response code="204">Sinaliza um sucesso sem conteúdo no corpo da response</response>
        /// <response code="400">Erro de validação do param carId</response>
        /// <response code="404">Id do carro não encontrado</response>
        /// <response code="500">Informa sobre erro interno do serviço</response> 
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