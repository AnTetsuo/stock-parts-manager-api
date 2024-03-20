using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using stock_manager_api.Dto;

namespace stock_manager_api.Controllers
{
    [ApiController]
    [Route("budgets")]
    public class BudgetController : Controller
    {
        private readonly BudgetRepository budgetRepository;
        public BudgetController(BudgetRepository repository) { budgetRepository = repository; }

        /// <summary>
        /// Lista os orçamentos registrados
        /// </summary>
        /// <response code="200">Retorna os orçamentos com o carro, o cliente e as peças</response>
        /// <response code="500">Informa sobre um erro interno do serviço</response>
        [HttpGet(Name = "GetBudget")]
        public IActionResult GetBudget()
        {
            try
            {
                return Ok(budgetRepository.GetAll());
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Busca um orçamento por Id
        /// </summary>
        /// <param name="budgetId">O id do orçamento a ser buscado</param>
        /// <response code="200">Retorna o orçamento com a placa do carro, o cliente e as peças</response>
        /// <response code="400">Erro de validação do param budgetId</response>
        /// <response code="404">Id do orçamento não encontrado</response>
        /// <response code="500">Informa sobre um erro interno do serviço</response>
        [HttpGet("{budgetId}")]
        public IActionResult GetBudgetById(int budgetId)
        {
            try
            {
                return Ok(budgetRepository.GetById(budgetId));
            }
            catch (KeyNotFoundException budgetNotFound)
            {
                return NotFound(budgetNotFound.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        /// <summary>
        /// Registra um orçamento alocando Peças em espera e removendo-as do estoque
        /// </summary>
        /// <param name="budget">Payload com as informações a serem registradas</param>
        /// <response code="201">Retorna o orçamento criado</response>
        /// <response code="202">Retorna uma mensagem informando que não há peças para o orçamento, não movimentando estoque</response>
        /// <response code="400">Erro de validação da payload</response>
        /// <response code="404">Entidade Cliente, Carro ou Peça não encontrada pelo id da payload</response>
        /// <response code="500">Informa sobre um erro interno do serviço</response>
        [HttpPost]
        public IActionResult PostBudget([FromBody] InsertBudgetDto budget)
        {
            try
            {
                ResponseBudgetDto createdBudget = budgetRepository.Add(budget);

                return CreatedAtRoute("GetBudget", createdBudget);
            }            
            catch (ArgumentException outOfStockEx)
            {
                return Accepted(outOfStockEx.Message);
            }
            catch (KeyNotFoundException ResourceNotFound)
            {
                return NotFound(ResourceNotFound.Message);
            }

        }


        /// <summary>
        /// Edita as peças de um orçamento.
        /// </summary>
        /// <param name="budgetId">O id do orçamento</param>
        /// <param name="budget">A payload com as novas informações para registro</param>
        /// <response code="201">Retorna o orçamento com as novas informações</response>
        /// <response code="202">Retorna uma mensagem informando que não há peças para orçamento, 
        /// não movimentando estoque ou alterando o orçamento</response>
        /// <response code="400">Erro de validação da payload ou do param budgetId</response>
        /// <response code="500">Informa sobre um erro interno do serviço</response>
        [HttpPut("{budgetId}")]
        public IActionResult EditBduget(int budgetId, [FromBody]InsertBudgetDto budget)
        {
            try
            {
                ResponseBudgetDto editedBduget = budgetRepository.Update(budget, budgetId);

                return CreatedAtRoute("GetBudget", editedBduget);
            }
            catch (ArgumentException outOfStockEx)
            {
                return Accepted(outOfStockEx.Message);
            }
            catch (KeyNotFoundException budgetNotFound)
            {
                return NotFound(budgetNotFound.Message);
            }
            catch (Exception)
            {   
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        /// <summary>
        /// Remove um orçamento do registro realocando as peças em estoque
        /// </summary>
        /// <param name="budgetId">O id do orçamento a ser removido</param>
        /// <response code="204">Sinaliza sucesso sem conteúdo no corpo da requisição</response>
        /// <response code="400">Erro de validação do param budgetId</response>
        /// <response code="404">Id do orçamento não encontrado</response>
        /// <response code="500">Informa sobre um erro interno do serviço</response>
        [HttpDelete("{budgetId}")]
        public IActionResult DeleteBudget(int budgetId)
        {
            try
            {
                budgetRepository.Delete(budgetId);

                return NoContent();
            }
            catch (KeyNotFoundException budgetNotFound)
            {
                return NotFound(new { message = budgetNotFound.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}