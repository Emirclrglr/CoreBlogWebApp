using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using X.PagedList;
namespace CoreBlogWebApp.Controllers
{
    public class MessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EFMessageDal());
        public IActionResult Inbox(int page = 1)
        {
            using var c = new Context();
            var user = User.Identity.Name;
            var mail = c.Users.Where(x => x.UserName == user).Select(y => y.Email).FirstOrDefault();
            var authUserId = c.Authors.Where(x => x.AuthorMail == mail).Select(y => y.AuthorId).FirstOrDefault();
            var values = messageManager.TGetMessagesByAuthorId(authUserId).ToPagedList(page, 10);
            return View(values);
        }

        public IActionResult SendBox(int page = 1)
        {
            using var c = new Context();
            var user = User.Identity.Name;
            var mail = c.Users.Where(x=>x.UserName == user).Select(y=>y.Email).FirstOrDefault();
            var authId = c.Authors.Where(x=>x.AuthorMail == mail).Select(y=>y.AuthorId).FirstOrDefault();
            var values = messageManager.TGetMessagesBySender(authId).ToPagedList(page,10);
            return View(values);
        }

        public IActionResult MessageDelete(int id)
        {
            var getId = messageManager.GetById(id);
            messageManager.Delete(getId);
            return RedirectToAction("Inbox");
        }
        public IActionResult MessageDetails(int id)
        {
            var values = messageManager.TGetByIdWithSenderName(id);
            return View(values);
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendMessage(Message2 p)
        {
            using var c = new Context();
            var user = User.Identity.Name;
            var mail = c.Users.Where(x => x.UserName == user).Select(y => y.Email).FirstOrDefault();
            var authUserId = c.Authors.Where(x => x.AuthorMail == mail).Select(y => y.AuthorId).FirstOrDefault();
            p.SenderId = authUserId;
            p.Status = true;
            p.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            messageManager.Insert(p);
            return RedirectToAction("Inbox");
        }


    }
}
