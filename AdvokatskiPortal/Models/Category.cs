using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CraftmanPortal.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("ParentCategory")]
        public int? ParentId { get; set; }
        public Category ParentCategory { get; set; }
        
        public ICollection<ContractCategory> ContractCategories { get; set; }
        public ICollection<Case> Cases { get; set; }
    }
}
