using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.Controllers
{
	[AllowAnonymous]
	public class AboutController : Controller
    {
        AboutManager aboutManager = new AboutManager(new EFAboutDal());
        public IActionResult Index()
        {
            var values = aboutManager.GetList();
            return View(values);
        }

        public PartialViewResult SocialMediaAccountsPartial()
        {
            return PartialView();
        }
    }
}
