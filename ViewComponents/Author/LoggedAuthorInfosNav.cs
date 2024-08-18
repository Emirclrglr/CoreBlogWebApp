using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.ViewComponents.Author
{
    public class LoggedAuthorInfosNav : ViewComponent
    {
        [Authorize]
        public IViewComponentResult Invoke()
        {
            using var c = new Context();
            var user = User.Identity.Name;
            var mail = c.Users.Where(x=>x.UserName == user).Select(y=>y.Email).FirstOrDefault();
            var authorFirstName = c.Users.Where(x => x.Email == mail).Select(y => y.Firstname).FirstOrDefault();
            var authorLastName = c.Users.Where(x => x.Email == mail).Select(y => y.Lastname).FirstOrDefault();
            var authorImg = c.Users.Where(x => x.Email == mail).Select(y => y.ImgUrl).FirstOrDefault();
            ViewBag.v1 = authorFirstName;
            ViewBag.v3 = authorLastName;
            ViewBag.v2 = authorImg;
            return View();
        }
    }
}
