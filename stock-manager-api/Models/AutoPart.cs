using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using stock_manager_api.Dto;

namespace stock_manager_api.Models
{
    public class AutoPart 
    {
        [Key]
        [Column("auto_part_id")]
        public int AutoPartId { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("quantity")]
        public required int Stock { get; set; }

        [Column("budgeted")]
        public required int Budgeted { get; set; }

        public ResponseAutoPartDto ToDto()
        {
            return new ResponseAutoPartDto 
            {
                id = AutoPartId,
                name = Name,
                budgeted = Budgeted,
                stock = Stock 
            };
        } 
    }
}