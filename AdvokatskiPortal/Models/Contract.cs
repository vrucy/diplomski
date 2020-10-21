using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractorskiPortal.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string TypeOfPayment { get; set; }
        //not good name
        public string QuantitySize { get; set; }
        public string Comment { get; set; }
        //public bool isKonacan { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime? ReciveCase { get; set; }
        public DateTime? ChangeCaseDate { get; set; }
        [ForeignKey("Case")]
        public int CaseId { get; set; }
        public Case Case { get; set; }
        public int StatusId{ get; set; }
        public Status Status { get; set; }
        [ForeignKey("Contractor")]
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

    }
}
