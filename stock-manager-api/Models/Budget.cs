using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Client? Client { get; set; }

        public Car? Car { get; set; }

    }
}
