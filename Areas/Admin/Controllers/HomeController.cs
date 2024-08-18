using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
