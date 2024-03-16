using Microsoft.AspNetCore.Mvc;
using stock_manager_api.Dto;
using stock_manager_api.Repository;

namespace stock_manager_api.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientController : Controller
    {
        private readonly ClientRepository clientRepository;

        public ClientController(ClientRepository repository) { clientRepository = repository; }

        [HttpGet(Name = "GetClients")]
        public IActionResult GetClients()
        {
            try
            {
                return Ok(clientRepository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpGet("{clientId}")]
        public IActionResult GetClintById(int clientId)
        {
            try
            {
                ResponseClientDto clientById = clientRepository.GetById(clientId);

                return Ok(clientById);
            }
            catch (KeyNotFoundException ClientNotFound)
            {
                return NotFound(ClientNotFound.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpPost]
        public IActionResult PostClient([FromBody] InsertClientDto client)
        {
            try
            {
                ResponseClientDto createdClient = clientRepository.Add(client);

                return CreatedAtRoute("GetClients", createdClient);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpPut("{clientId}")]
        public IActionResult PutClient(int clientId, [FromBody] InsertClientDto client)
        {
            try
            {
                ResponseClientDto clientEdit = clientRepository.Update(client, clientId);

                return CreatedAtRoute("GetClients", clientEdit);
            }
            catch (KeyNotFoundException ClientNotFound)
            {
                return NotFound(ClientNotFound.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpDelete("{clientId}")]
        public IActionResult DeleteClient(int clientId)
        {
            try
            {
                clientRepository.Delete(clientId);

                return NoContent();
            }
            catch (KeyNotFoundException ClientNotFound)
            {
                return NotFound(ClientNotFound.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
    }
}