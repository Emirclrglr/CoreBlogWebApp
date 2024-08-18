using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.ViewComponents.Notification
{
    public class AuthorNotificationList : ViewComponent
    {
        NotificationManager notificationManager = new NotificationManager(new EFNotificationDal());
        public IViewComponentResult Invoke()
        {
            var values = notificationManager.GetList();
            return View(values);
        }
    }
}
