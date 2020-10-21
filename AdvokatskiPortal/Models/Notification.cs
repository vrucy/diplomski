using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContractorskiPortal.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool isRead { get; set; }
        public string NotificationText { get; set; }
        public DateTime TimeStamp { get; set; }
        [ForeignKey("Case")]
        public int CaseId{ get; set; }
        public Case Case { get; set; }
    }
}
