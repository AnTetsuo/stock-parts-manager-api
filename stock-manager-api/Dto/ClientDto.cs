using System.ComponentModel.DataAnnotations;

namespace stock_manager_api.Dto
{
    public class InsertClientDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name needs to be at least 2 characters long")]
        public required string Name { get; set;}
    }

    public class ResponseClientDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "id needs to be a valid Integer number")]
        public required int id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Name needs to be at least 2 characters long")]
        public required string name { get; set; }
    }
}