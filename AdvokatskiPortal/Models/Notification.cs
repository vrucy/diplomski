using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvokatskiPortal.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool isRead { get; set; }
        public string NotificationText { get; set; }
        public DateTime TimeStamp { get; set; }
        [ForeignKey("Slucaj")]
        public int SlucajId{ get; set; }
        public Slucaj Slucaj { get; set; }
    }
}
