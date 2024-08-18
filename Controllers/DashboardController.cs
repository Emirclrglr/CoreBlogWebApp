using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.Controllers
{
    public class DashboardController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }


    }
}
