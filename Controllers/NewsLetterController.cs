using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.Controllers
{
    [AllowAnonymous]
    public class NewsLetterController : Controller
    {
        NewsletterManager newsletterManager = new NewsletterManager(new EFNewsletterDal());
        [HttpGet]
        public PartialViewResult SubscribeToNewsletter()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult SubscribeToNewsletter(Newsletter newsletter)
        {
			newsletter.MailStatus = true;

			newsletterManager.Insert(newsletter);
            return PartialView();
        }

    }
}
