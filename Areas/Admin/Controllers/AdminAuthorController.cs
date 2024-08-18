using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using CoreBlogWebApp.Areas.Admin.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAuthorController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AdminAuthorController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        UserManager userManager = new(new EFUserDal());
        public IActionResult Index()
        {
            var values = userManager.GetList();
            return View(values);
        }

    }
}
