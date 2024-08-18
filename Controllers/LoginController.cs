using BusinessLayer.Concrete;
using CoreBlogWebApp.Models;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoreBlogWebApp.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserLoginViewModel p)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.Username, p.Password, false, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Login");
        }

        #region Claims
        //[HttpPost]
        //public async Task<IActionResult> Index(Author author)
        //{
        //	Context context = new Context();
        //	var dataValue = context.Authors.FirstOrDefault(x => x.AuthorMail == author.AuthorMail && x.AuthorPassword == author.AuthorPassword);
        //	if (dataValue != null)
        //	{
        //		var claims = new List<Claim>()
        //		{
        //			new Claim(ClaimTypes.Name, author.AuthorMail),

        //		};

        //		var userIdentity = new ClaimsIdentity(claims, "a");
        //		ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
        //		await HttpContext.SignInAsync(principal);
        //		return RedirectToAction("Index", "Dashboard");
        //	}
        //	else
        //	{
        //		return View();
        //	}
        //}
        #endregion


    }
}
