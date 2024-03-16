using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Budget? Budget { get; set; }

        [ForeignKey("AutoPartId")]
        public AutoPart? AutoPart { get; set; }

    }
}

