using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.ViewComponents.Author
{
    [Authorize]
    public class LoggedAuthorInfosSide : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;


        public IViewComponentResult Invoke()
        {
            using var c = new Context();
            var user = User.Identity.Name;
            var userMail = c.Users.Where(x => x.UserName == user).Select(y => y.Email).FirstOrDefault();
            var authorFirstName =  c.Users.Where(x=>x.Email == userMail).Select(y=>y.Firstname).FirstOrDefault();
            var authorLastName =  c.Users.Where(x=>x.Email == userMail).Select(y=>y.Lastname).FirstOrDefault();
            var authorImg =  c.Users.Where(x=>x.Email == userMail).Select(y=>y.ImgUrl).FirstOrDefault();
            ViewBag.v1 = authorFirstName;
            ViewBag.v3 = authorLastName;
            ViewBag.v2 = authorImg;
            return View();
        }
    
    }
}
