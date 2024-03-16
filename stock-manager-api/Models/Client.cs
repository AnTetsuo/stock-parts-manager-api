using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
