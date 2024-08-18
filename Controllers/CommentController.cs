using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreBlogWebApp.Controllers
{
	[AllowAnonymous]
	public class CommentController : Controller
	{
		CommentManager commentManager = new CommentManager(new EFCommentDal());
		public IActionResult Index()
		{
			return View();
		}
        [HttpGet]
		public PartialViewResult AddComment()
		{
			return PartialView();
		}
		[HttpPost]
		public PartialViewResult AddComment(Comment comment)
		{
			comment.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
			comment.CommentStatus = true;
			comment.BlogId = 2;
			commentManager.Insert(comment);
			return PartialView();
		}
	}
}
