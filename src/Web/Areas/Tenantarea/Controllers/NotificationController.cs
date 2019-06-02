using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TenancyContract.Entities;
using Web.Repository;

namespace Web.Areas.Tenantarea.Controllers
{
    [Authorize(Policy = "OnlyTenant")]
    [Area(nameof(Tenantarea))]
    public class NotificationController : Controller
    {
       private INotificationRepository _notificationRepository;
        private UserManager<Tenant> _userManager;
        public NotificationController(INotificationRepository notificationRepository,
                                        UserManager<Tenant> userManager)
        {
            _notificationRepository = notificationRepository;
            _userManager = userManager;
        }

        public IActionResult GetNotification(){
            string nid = User.Claims.ElementAt(1).Value;
            var notification = _notificationRepository.GetUserNotifications(nid);
            return Ok(new{UserNotification = notification, Count = notification.Count});
        }

        public IActionResult ReadNotification(int notificationId){

            _notificationRepository.ReadNotification(notificationId,User.Claims.ElementAt(1).Value);

            return Ok();
}
    }
}