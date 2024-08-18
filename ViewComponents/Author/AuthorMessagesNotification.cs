using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.ViewComponents.Author
{
    public class AuthorMessagesNotification:ViewComponent
    {
        MessageManager messageManager = new MessageManager(new EFMessageDal());
        public IViewComponentResult Invoke()
        {
            using var c = new Context();
            var user = User.Identity.Name;
            var mail = c.Users.Where(x=>x.UserName == user).Select(y=>y.Email).FirstOrDefault();
            var authUserId = c.Authors.Where(x => x.AuthorMail == mail).Select(y => y.AuthorId).FirstOrDefault();
            ViewBag.v1 = c.Messages2.Where(x => x.ReceiverId == authUserId).Count();
            var values = messageManager.TGetMessagesByAuthorId(authUserId);
            return View(values);

        }
    }
}
