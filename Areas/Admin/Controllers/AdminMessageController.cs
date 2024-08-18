using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CoreBlogWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminMessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EFMessageDal());
        public IActionResult Inbox()
        {
            using var c = new Context();
            var user = User.Identity.Name;
            var mail = c.Users.Where(x => x.UserName == user).Select(y => y.Email).FirstOrDefault();
            var id = c.Authors.Where(x => x.AuthorMail == mail).Select(y => y.AuthorId).FirstOrDefault();
            ViewBag.count = messageManager.TGetMessagesByAuthorId(id).Count();
            var values = messageManager.TGetMessagesByAuthorId(id);
            return View(values);
        }

        public IActionResult SendBox()
        {
            using var c = new Context();
            var user = User.Identity.Name;
            var mail = c.Users.Where(x => x.UserName == user).Select(y => y.Email).FirstOrDefault();
            var authId = c.Authors.Where(x => x.AuthorMail == mail).Select(y => y.AuthorId).FirstOrDefault();
            var values = messageManager.TGetMessagesBySender(authId);
            return View(values);
        }

        [HttpGet]
        public IActionResult ComposeMessage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ComposeMessage(Message2 p)
        {
            using var c = new Context();
            var user = User.Identity.Name;
            var mail = c.Users.Where(x => x.UserName == user).Select(y => y.Email).FirstOrDefault();
            var authUserId = c.Authors.Where(x => x.AuthorMail == mail).Select(y => y.AuthorId).FirstOrDefault();
            p.SenderId = authUserId;
            p.Status = true;
            p.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            messageManager.Insert(p);
            return Redirect("/Admin/AdminMessage/SendBox"); 
        }

    }
}
