using System.Collections.Generic;
using Web.Entities;

namespace Web.Repository
{
    public interface INotificationRepository
    {
         List<NotificationApplicationUser> GetUserNotifications(string userId);
         void Create(Notification notification, string tenantNID, string houseOwnerNID);
         void ReadNotification(int notificationId, string userId);
    }
}