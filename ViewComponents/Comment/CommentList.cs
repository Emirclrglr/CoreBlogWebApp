using Blog.Controllers;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.ViewComponents.Comment
{
    public class CommentList : ViewComponent
    {
        CommentManager commentManager = new CommentManager(new EFCommentDal());
        public IViewComponentResult Invoke(int id)
        {
        
            var values = commentManager.GetList(id);
            return View(values);
        }
    }
}
