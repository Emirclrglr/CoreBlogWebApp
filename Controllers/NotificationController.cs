using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.Controllers
{
    public class NotificationController : Controller
    {
        NotificationManager notificationManager = new NotificationManager(new EFNotificationDal());
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllNotifications()
        {
            var values = notificationManager.GetList();
            return View(values);
        }

        public IActionResult Readed(int id)
        {
            notificationManager.TSetStatusReaded(id);
            return RedirectToAction("AllNotifications", "Notification");
        }

        public IActionResult UnReaded(int id)
        {
            notificationManager.TSetStatusUnReaded(id);
            return RedirectToAction("AllNotifications", "Notification");

        }
    }
}
