using CraftmanPortal.Data;
using CraftmanPortal.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CraftmanPortal.Extensons
{
    public static class NotificationExtensions
    {
        public static Notification CreateNewNotification(this CaseCraftman c, string action, string firstName)
        {
            var notification = new Notification
            {
                UserId = c.CraftmanIdIndentity,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                isRead = false,
                CaseId = c.CaseId,
                NotificationText = $"{firstName} is {action} Case:  {c.Case.Name}"
            };

            return notification;
        }
        public static Notification RejectNotification(this CaseCraftman c)
        {
            var notification = new Notification
            {
                UserId = c.Craftman.Idenity.Id,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                isRead = false,
                CaseId = c.CaseId,
                NotificationText = $"{c.Case.User.FirstName} is rejected Case:  {c.Case.Name}"
            };

            return notification;
        }
        public static Notification AcceptNotification(this Craftman c, Case Case)
        {
            var notification = new Notification
            {
                UserId = c.Idenity.Id,
                TimeStamp = DateTime.UtcNow.ToLocalTime(),
                isRead = false,
                CaseId = Case.Id,
                NotificationText = $"{Case.User.FirstName} is accepted Case:  {Case.Name}"
            };

            return notification;
        }
    }
}
