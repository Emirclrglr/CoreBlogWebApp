using BusinessLayer.Concrete;
using CoreBlogWebApp.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreBlogWebApp.Controllers
{
    public class AuthorController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        AuthorManager authorManager = new AuthorManager(new EFAuthorDal());

        public AuthorController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userMail = User.Identity.Name;
            ViewBag.v = userMail;
            using var c = new Context();
            var authorName = c.Authors.Where(x => x.AuthorMail == userMail).Select(y => y.AuthorName).FirstOrDefault();
            ViewBag.v2 = authorName;
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }

        public PartialViewResult NavbarPartial()
        {
            return PartialView();
        }

        public PartialViewResult SidebarPartial()
        {
            return PartialView();
        }

        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }

        public PartialViewResult HeadPartial()
        {
            return PartialView();
        }

        public PartialViewResult ScriptPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> ProfileSettings()
        {
            #region 1. Yöntem
            //using var c = new Context();
            //UserManager userManager = new UserManager(new EFUserDal());
            //var userName = User.Identity.Name;
            //var mail = c.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            //var id = c.Users.Where(x => x.Email == mail).Select(y => y.Id).FirstOrDefault();
            //var values = userManager.GetById(id);
            //return View(values);
            #endregion

            #region 2. Yöntem
            var val = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel();
            model.Firstname = val.Firstname;
            model.Lastname = val.Lastname;
            model.Mail = val.Email;
            model.Username = val.UserName;
            model.ImgUrl = val.ImgUrl;
            return View(model);
            #endregion

        }
        [HttpPost]
        public async Task<IActionResult> ProfileSettings(UserUpdateViewModel model)
        {
            var userValues = await _userManager.FindByNameAsync(User.Identity.Name);
            userValues.Firstname = model.Firstname;
            userValues.Lastname = model.Lastname;
            userValues.Email = model.Mail;
            userValues.PasswordHash = _userManager.PasswordHasher.HashPassword(userValues, model.Password);
            var result = await _userManager.UpdateAsync(userValues);
            if (result.Succeeded)
            {
                return RedirectToAction("ProfileSettings", "Author");
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAuthor(AddProfilePicture p)
        {
            Author author = new Author();
            if (p.AuthorImg is not null)
            {
                var extension = Path.GetExtension(p.AuthorImg.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/AuthorImageFiles/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                p.AuthorImg.CopyTo(stream);
                author.AuthorImg = newImageName;
            }
            author.AuthorMail = p.AuthorMail;
            author.AuthorName = p.AuthorName;
            author.AuthorPassword = p.AuthorPassword;
            author.AuthorStatus = true;
            author.AuthorAbout = p.AuthorAbout;
            authorManager.Insert(author);
            return RedirectToAction("Index", "Dashboard");
        }

        
    }
}
