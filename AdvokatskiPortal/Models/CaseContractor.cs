using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CraftmanPortal.Models
{
    public class CaseCraftman
    {
        
        public DateTime CreationDate { get; set; }
        public int CraftmanId { get; set; }
        public string CraftmanIdIndentity { get; set; }
        public Craftman Craftman { get; set; } 
        public int CaseId { get; set; }
        public Case Case{ get; set; }
        
        [ForeignKey("CaseStatus")]
        public int CaseStatusId { get; set; }
        public CaseStatus CaseStatus { get; set; }
    }
}
