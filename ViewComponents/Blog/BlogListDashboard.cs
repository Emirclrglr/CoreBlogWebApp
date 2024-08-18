using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.ViewComponents.Blog
{
    public class BlogListDashboard:ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogDal());
        public IViewComponentResult Invoke()
        {
            var values = blogManager.TGetListWithCategory().TakeLast(10).OrderByDescending(x => x.BlogCreateDate).ToList(); 
            return View(values);
        }
    }
}
