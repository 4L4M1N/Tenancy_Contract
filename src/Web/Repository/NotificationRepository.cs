using System.Collections.Generic;
using Web.Entities;
using Tenancy_Contract.DataAccessLayer;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Web.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly TenancyContractDbContext _db;
        public NotificationRepository(TenancyContractDbContext db)
        {
            _db = db;
        }
        public void Create(Notification notification, string tenantNID, string houseOwnerNID)
        {
            _db.Notifications.Add(notification);
            _db.SaveChanges();
            var NotificationApplicationUser =new NotificationApplicationUser {
                    TenantNID = tenantNID,
                    HouseOwnerNID = houseOwnerNID,
                    NotificationId = notification.Id
            };
            _db.NotificationApplicationUsers.Add(NotificationApplicationUser);
            _db.SaveChanges();
        }

        public List<NotificationApplicationUser> GetUserNotifications(string userId)
        {
            return _db.NotificationApplicationUsers.Where(u=> u.TenantNID.Equals(userId) && !u.IsRead)
            .Include(n=> n.Notification)
            .ToList();
        }

        public void ReadNotification(int notificationId, string userId)
        {
            var notification = _db.NotificationApplicationUsers
                                        .FirstOrDefault(n=>n.TenantNID.Equals(userId) 
                                        && n.NotificationId==notificationId);
            notification.IsRead = true;
            _db.NotificationApplicationUsers.Update(notification);
            _db.SaveChanges();
        }
    }
}