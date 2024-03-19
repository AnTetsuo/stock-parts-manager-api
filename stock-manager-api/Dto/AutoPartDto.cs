using System.ComponentModel.DataAnnotations;

namespace stock_manager_api.Dto
{
    public class InsertAutoPartDto
    {
        [Required]
        [Length(1, 128, ErrorMessage = "Name needs to be between 1 and 128 characters long")]
        public required string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = $"Quantity needs to be a valid Integer number")]
        public int Quantity { get; set; }
        [Required]
        [AllowedValues(0, ErrorMessage = "Budgeted only accepts the default value of '0'")]
        public int Budgeted { get; set; }
    }

    public class AddAutoPartToBudgetDto
    {
        [Required]
        public required int Id { get; set; }
        [Required]
        [Length(1, 128, ErrorMessage = "Name needs to be between 1 and 128 characters long")]
        public required string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity needs to a valid Integer number")]
        public required int Quantity { get; set; }
    }

    public class ResponseBudgetedAutoPartsDto
    {
        public required int id { get; set; }
        public required string name { get; set; }
        public required int quantity { get; set; }
    }


    public class ResponseAutoPartDto
    {
        public required int id { get; set; }
        public required string name { get; set; }
        public required int stock { get; set; }
        public required int budgeted { get; set; }
    }
}