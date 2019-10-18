using System;
using System.Collections.Generic;
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
    }
}
