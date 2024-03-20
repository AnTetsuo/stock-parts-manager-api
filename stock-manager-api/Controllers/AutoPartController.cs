using Microsoft.AspNetCore.Mvc;
using stock_manager_api.Dto;
using stock_manager_api.Repository;

namespace stock_manager_api.Controllers
{
    [ApiController]
    [Route("autoparts")]
    public class AutoPartController : Controller
    {
        private readonly AutoPartRepository autoPartRepository;

        public AutoPartController(AutoPartRepository repository) { autoPartRepository = repository; }

        /// <summary>
        /// Lista todas as peças cadastradas
        /// </summary>
        /// <response code="200">Retorna a lista das peças com id, nome, estoque e orçadas</response>
        /// <response code="500">Informa sobre erro interno do serviço</response>
        [HttpGet(Name = "GetAutoPart")]
        public IActionResult GetAutoParst()
        {
            try
            {
                return Ok(autoPartRepository.GetAll());
            }
            catch(Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
        
        /// <summary>
        /// Busca uma peça por id
        /// </summary>
        /// <param name="autoPartId">O id da peça a ser buscada</param>
        /// <response code="200">Retorna a peça com id, nome, estoque e orçadas</response>
        /// <response code="400">Erro de validação do param autoPartId</response>
        /// <response code="404">Id da peça não encontrado</response>
        /// <response code="500">Informa sobre erro interno do serviço</response>
        [HttpGet("{autoPartId}")]
        public IActionResult GetAutoPartById(int autoPartId)
        {
            try
            {
                return Ok(autoPartRepository.GetById(autoPartId));
            }
            catch(KeyNotFoundException autoPartNotFound)
            {
                return NotFound(autoPartNotFound.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        /// <summary>
        /// Registra uma peça
        /// </summary>
        /// <param name="autopart">A payload com o 
        /// nome, estoque e quantidade em orçamento[budgeted](Apenas 0 é permitido)</param>
        /// <response code="201">Retorna a peça com id, nome, estoque e orçadas registrada</response>
        /// <response code="400">Erro de validação da payload autopart</response>
        /// <response code="500">Informa sobre erro interno do serviço</response>
        [HttpPost]
        public IActionResult PostAutoPart([FromBody] InsertAutoPartDto autopart)
        {
            try
            {
                ResponseAutoPartDto createdAutoPart = autoPartRepository.Add(autopart);

                return CreatedAtRoute("GetAutoPart", createdAutoPart);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }


        /// <summary>
        /// Edita apenas o nome da peça
        /// </summary>
        /// <param name="autoPartId">O id da peça a ser editada</param>
        /// <param name="autoPart">A payload com o nome, estoque e quantidade em orçamento[budgeted](Apenas 0 é permitido)</param>
        /// <response code="201">Retorna a peça com nome editado, id, estoque e orçadas</response>
        /// <response code="400">Erro de validação da payload autoPart</response>
        /// <response code="404">Id da peça a ser editada não encontrado</response>
        /// <response code="500">Informa sobre erro interno do serviço</response>        
        [HttpPut("{autoPartId}")]
        public IActionResult RenameAutoPart(int autoPartId,[FromBody] InsertAutoPartDto autoPart)
        {
            try
            {
                ResponseAutoPartDto editAutoPart = autoPartRepository.Update(autoPart, autoPartId);

                return CreatedAtRoute("GetAutoPart", editAutoPart);
            }
            catch (KeyNotFoundException autoPartNotFound)
            {
                return NotFound(autoPartNotFound.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        /// <summary>
        /// Remove uma peça
        /// </summary>
        /// <param name="autoPartId">O id da peça a ser removida</param>
        /// <response code="204">Sinaliza sucesso e response sem conteúdo</response>
        /// <response code="400">Erro de validação do param autoPartId</response>
        /// <response code="404">Id da peça não encontrado</response>
        /// <response code="500">Informa sobre erro interno do serviço</response>        
        [HttpDelete("{autoPartId}")]
        public IActionResult DeleteAutoPart(int autoPartId)
        {
            try
            {
                autoPartRepository.Delete(autoPartId);

                return NoContent();
            }
            catch (KeyNotFoundException autoPartNotFound)
            {
                return NotFound(autoPartNotFound.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
    }
}