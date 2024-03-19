using System.ComponentModel.DataAnnotations;

namespace stock_manager_api.Dto
{
    public class InsertBudgetDto
    {
        [Required]
        public required ResponseClientDto Client { get; set; }
        [Required]
        public required ResponseCarDto Car { get; set; }
        [Required]
        public required IEnumerable<AddAutoPartToBudgetDto> Parts { get; set; }
    }

    public class ResponseBudgetDto
    {
        public required int id { get; set; }
        public required ResponseCarDto car { get; set; }
        public required ResponseClientDto client { get; set; }
        public required IEnumerable<ResponseBudgetedAutoPartsDto> autoparts { get; set; }
    }
}