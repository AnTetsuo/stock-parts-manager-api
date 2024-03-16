namespace stock_manager_api.Dto
{
    public class InsertCarDto
    {
        public required string Plate { get; set;}
    }

    public class ResponseCarDto
    {
        public required int id { get; set; }
        public required string plate { get; set; }
    }
}