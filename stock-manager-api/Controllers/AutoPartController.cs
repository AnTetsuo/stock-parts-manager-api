using System.Net.NetworkInformation;
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