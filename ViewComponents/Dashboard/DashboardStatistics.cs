using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.ViewComponents.Dashboard
{
    public class DashboardStatistics:ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogDal());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryDal());
        public IViewComponentResult Invoke()
        {
            using var c = new Context();
            var user = User.Identity.Name;
            var mail = c.Users.Where(x=>x.UserName == user).Select(y=>y.Email).FirstOrDefault();
            var authUserId = c.Authors.Where(x => x.AuthorMail == mail).Select(y => y.AuthorId).FirstOrDefault();
            var values = blogManager.TGetListByAuthorWithCategoryList(authUserId);
            ViewBag.st1 = blogManager.BlogCount();
            ViewBag.st2 = blogManager.BlogCountByAuthor(authUserId);
            ViewBag.st3 = categoryManager.CategoryCount();
            return View();
        }
    }
}
