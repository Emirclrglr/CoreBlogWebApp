using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.ViewComponents.Blog
{
	public class Last3PostsFooter : ViewComponent
	{
		BlogManager blogManager = new BlogManager(new EFBlogDal());
		public IViewComponentResult Invoke()
		{
			var values = blogManager.GetLast3Post();
			return View(values);
		}
	}
}
