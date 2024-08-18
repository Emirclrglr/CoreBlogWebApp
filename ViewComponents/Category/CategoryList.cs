using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.ViewComponents.Category
{
    public class CategoryList:ViewComponent
    {
        CategoryManager categoryManager = new CategoryManager(new EFCategoryDal());
        public IViewComponentResult Invoke()
        {
            List<EntityLayer.Concrete.Category> values = categoryManager.GetList();
            return View(values);
        }
    }
}
