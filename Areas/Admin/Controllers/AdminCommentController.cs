using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCommentController : Controller
    {
        CommentManager commentManager = new CommentManager(new EFCommentDal());
        public IActionResult Index()
        {
            var values = commentManager.TGetListWithBlog();
            return View(values);
        }

        public IActionResult StatusActive(int id)
        {
            commentManager.TSetStatusActive(id);
            return RedirectToAction("Index");
        }
        public IActionResult StatusPassive(int id)
        {
            commentManager.TSetStatusPassive(id);
            return RedirectToAction("Index");
        }
    }
}
