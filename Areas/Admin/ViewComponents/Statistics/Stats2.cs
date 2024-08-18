using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.Areas.Admin.ViewComponents.Statistics
{
    public class Stats2:ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogDal());
        Context context = new Context();
        public IViewComponentResult Invoke()
        {
            ViewBag.LastAddedBlog = context.Blogs.ToList().Select(x=>x.BlogTitle).TakeLast(1).FirstOrDefault();
            return View();
        }

    }
}
