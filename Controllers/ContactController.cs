using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.Controllers
{
	[AllowAnonymous]
	public class ContactController : Controller
	{
		ContactManager contactManager = new ContactManager(new EFContactDal());
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(Contact contact)
		{
			contact.Status = true;
			contact.ContactDate = DateTime.Parse(DateTime.Now.ToShortDateString());
			contactManager.Insert(contact);
			return RedirectToAction("Index", "Blog");
		}
	}
}
