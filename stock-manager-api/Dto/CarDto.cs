using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;

namespace stock_manager_api.Dto
{
    public class InsertCarDto
    {
        [RegularExpression(@"[A-Z]{3}[0-9]{4}",
            ErrorMessage = "Plate needs to be 3 Uppercase letters followed by 4 numbers")]
        public required string Plate { get; set;}
    }

    public class ResponseCarDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "id needs to be a valid Integer number")]
        public required int id { get; set; }
        [Required]
        [RegularExpression(@"[A-Z]{3}[0-9]{4}",
            ErrorMessage = "Plate needs to be 3 Uppercase letters followed by 4 numbers")]
        public required string plate { get; set; }
    }
}