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

        /// <summary>
        /// Lista o nome e o id de todos os clientes
        /// </summary>
        /// <response code="200">Retorna a lista com id e nome dos clientes</response>
        /// <response code="500">Informa sobre um erro interno do serviço</response>
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

        /// <summary>
        /// Busca um cliente por id
        /// </summary>
        /// <param name="clientId">O id a ser buscado</param>
        /// <response code="200">Retorna o id e nome do cliente encontrado</response>
        /// <response code="400">Erro de validação do param clientId</response>
        /// <response code="404">Id do cliente não encontrado</response>
        /// <response code="500">Infoma sobre um erro interno do serviço</response>
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

        /// <summary>
        /// Cadastra um cliente
        /// </summary>
        /// <param name="client">A payload contendo o nome do cliente</param>
        /// <response code="201">Retorna o recurso criado com id e nome</response>
        /// <response code="400">Erro de validação da payload</response>
        /// <response code="500">Informa sobre um erro interno do serviço</response>
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


        /// <summary>
        /// Edita um cliente
        /// </summary>
        /// <param name="clientId">O id do cliente a ser editado</param>
        /// <param name="client">A payload com o novo valor a ser registrado</param>
        /// <response code="201">Retorna o recurso com o id e nome editado</response>
        /// <response code="400">Erro de validação na payload ou param clientId</response>
        /// <response code="404">Id do cliente não encontrado</response>
        /// <response code="500">Informa sobre um erro interno do serviço</response>
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


        /// <summary>
        /// Remove um cliente
        /// </summary>
        /// <param name="clientId">O id do cliente a ser removido</param>
        /// <response code="204">Resposta Sinalizando sucesso, sem conteúdo</response>
        /// <response code="400">Erro de validação do param clientId</response>
        /// <response code="404">Id do cliente não encontrado</response>
        /// <response code="500">Informa sobre um erro interno do serviço</response>
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