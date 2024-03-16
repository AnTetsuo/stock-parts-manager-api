using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using stock_manager_api.Dto;

namespace stock_manager_api.Models
{
    public class Client
    {
        [Key]
        [Column("client_id")]
        public int ClientId { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        public ICollection<Budget>? Budgets { get; set; }

        public ResponseClientDto ToDto()
        {
            return new ResponseClientDto
            {
                id = ClientId,
                name = Name,
            };
        }
    }
}
