using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreBlogWebApp.Controllers
{
	[AllowAnonymous]
	public class RegisterController : Controller
    {
        AuthorManager authorManager = new AuthorManager(new EFAuthorDal());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Author author)
        {
            RegisterValidator validationRules = new RegisterValidator();
            ValidationResult result = validationRules.Validate(author);
            if(result.IsValid)
            {
                author.AuthorStatus = true;
                author.AuthorAbout = "Test";
                authorManager.Insert(author);
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                foreach (var items in result.Errors)
                {
                    ModelState.AddModelError(items.PropertyName, items.ErrorMessage);
                }
            }

            return View();
        }
    }
}
