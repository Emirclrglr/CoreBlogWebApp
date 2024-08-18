using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.ViewComponents.Blog
{
	public class OtherPostsOfThisAuthor:ViewComponent
	{
		BlogManager blogManager = new BlogManager(new EFBlogDal());
		public IViewComponentResult Invoke()
		{
			var values = blogManager.TGetBlogListWithAuthor(1);
			return View(values);
		}
	}
}
