using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractorskiPortal.Models
{
    public class CaseContractor
    {
        
        public DateTime CreationDate { get; set; }
        public int ContractorId { get; set; }
        public string ContractorIdIndentity { get; set; }
        public Contractor Contractor { get; set; } 
        public int CaseId { get; set; }
        public Case Case{ get; set; }
        
        [ForeignKey("CaseStatus")]
        public int CaseStatusId { get; set; }
        public CaseStatus CaseStatus { get; set; }
    }
}
