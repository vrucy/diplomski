using System.ComponentModel.DataAnnotations.Schema;

namespace CraftmanPortal.Models
{
    public class ContractCategory
    {
        [ForeignKey("Craftman")]
        public int CraftmanId { get; set; }
        public Craftman Craftman { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
