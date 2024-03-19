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