using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult NavbarPartial()
        {
            return PartialView();
        } 
        
    }
}
