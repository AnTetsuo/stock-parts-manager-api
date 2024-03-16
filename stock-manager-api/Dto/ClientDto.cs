namespace stock_manager_api.Dto
{
    public class InsertClientDto
    {
        public required string Name { get; set;}
    }

    public class ResponseClientDto
    {
        public required int id { get; set; }
        public required string name { get; set; }
    }
}