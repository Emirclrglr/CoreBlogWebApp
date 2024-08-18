using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        BlogManager blogManager = new BlogManager(new EFBlogDal());
        public IActionResult Index()
        {
            var values = blogManager.TGetListWithCategory();
            return View(values);
        }
    }
}
