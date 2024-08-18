using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.Areas.Admin.ViewComponents.Statistics
{
    public class Stats4 : ViewComponent
    {
        Context context = new Context();
        public IViewComponentResult Invoke()
        {
            ViewBag.AdminName = context.Admins.Where(x => x.AdminId == 1).Select(y => y.Name).FirstOrDefault();
            ViewBag.AdminImage = context.Admins.Where(x => x.AdminId == 1).Select(y => y.ImageUrl).FirstOrDefault();
            ViewBag.AdminShortDesc = context.Admins.Where(x => x.AdminId == 1).Select(y => y.ShortDescription).FirstOrDefault();
            return View();
        }
    }
}
