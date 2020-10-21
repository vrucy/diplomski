using System.ComponentModel.DataAnnotations.Schema;

namespace ContractorskiPortal.Models
{
    public class ContractCategory
    {
        [ForeignKey("Contractor")]
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
