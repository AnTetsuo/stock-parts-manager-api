using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using stock_manager_api.Dto;

namespace stock_manager_api.Models
{
    public class Budget 
    {
        [Key]
        public int BudgetId { get; set; }

        [ForeignKey("ClientId")]
        [Column("client_id")]
        public int ClientId { get; set; }

        [ForeignKey("CarId")]
        [Column("car_id")]
        public int CarId { get; set; }

        public required Client Client { get; set; }

        public required Car Car { get; set; }

        public required List<BudgetedPart> BudgetedParts {get; set;}

        public ResponseBudgetDto ToDto()
        {
            return new ResponseBudgetDto
            {
                id = BudgetId,
                client = Client.ToDto(),
                car = Car.ToDto(),
                autoparts =  BudgetedParts.Select(e => e.ToDto())
            };
        }
    }
}
