using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using stock_manager_api.Dto;

namespace stock_manager_api.Models 
{
    public class BudgetedPart 
    {
        [Key]
        [Column("budgeted_part_id")]
        public int BudgetedPartId { get; set; }

        [ForeignKey("AutoPartId")]
        [Column("auto_part_id")]
        public int AutoPartId { get; set; }

        [ForeignKey("BudgetId")]
        [Column("budget_id")]
        public int BudgetId { get; set; }

        [Column("quantity")]
        public required int Quantity { get; set; }

        [ForeignKey("BudgetId")]
        public required Budget Budget { get; set; }

        [ForeignKey("AutoPartId")]
        public required AutoPart AutoPart { get; set; }

        public ResponseBudgetedAutoPartsDto ToDto()
        {
            return new ResponseBudgetedAutoPartsDto
            {  
                id = AutoPartId,
                name = AutoPart.Name,
                quantity = Quantity,
            };
        }
    }
}

