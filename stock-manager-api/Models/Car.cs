using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stock_manager_api.Models 
{
    public class Car 
    {
        [Key]
        [Column("car_id")]
        public int CarId { get; set; }

        [Column("plate")]
        public required string Plate { get; set; }

        public ICollection<Budget>? Budgets { get; set; }
    }
}