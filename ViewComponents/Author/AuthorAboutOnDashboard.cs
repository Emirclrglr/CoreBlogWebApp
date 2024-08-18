using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.ViewComponents.Author
{
    public class AuthorAboutOnDashboard:ViewComponent
    {
        AuthorManager authorManager = new AuthorManager(new EFAuthorDal());

        public IViewComponentResult Invoke()
        {
            using var c = new Context();
            var user = User.Identity.Name;
            var mail = c.Users.Where(x=>x.UserName == user).Select(y=>y.Email).FirstOrDefault();
            var authUserId = c.Authors.Where(x=>x.AuthorMail ==  mail).Select(y=>y.AuthorId).FirstOrDefault();
            var values = authorManager.TGetListByAuthor(authUserId);
            return View(values);
        }
    }
}
