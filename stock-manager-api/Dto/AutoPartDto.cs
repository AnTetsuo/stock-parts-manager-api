namespace stock_manager_api.Dto
{
    public class InsertAutoPartDto
    {
        public required string Name { get; set; }
        public int Quantity { get; set; }
        public int Budgeted { get; set; }
    }

    public class ResponseAutoPartDto
    {
        public required int id { get; set; }
        public required string name { get; set; }
        public required int stock { get; set; }
        public required int budgeted { get; set; }
    }
}