using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CoreBlogWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EFCategoryDal());
        public IActionResult Index(int page = 1)
        {
            var values = categoryManager.GetList().ToPagedList(page, 10);
            return View(values);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            category.CategoryStatus = true;
            categoryManager.Insert(category);
            return RedirectToAction("Category", "Admin", "Index");
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var values = categoryManager.GetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            categoryManager.Update(category);
            return RedirectToAction("Category", "Admin", "Index");
        }

        public IActionResult DeleteCategory(int id)
        {
            var getId = categoryManager.GetById(id);
            categoryManager.Delete(getId);
            return RedirectToAction("Category", "Admin", "Index");
        }

        public IActionResult SetActive(int id)
        {
            categoryManager.TSetCategoryActive(id);
            return RedirectToAction("Category", "Admin", "Index");
        }

        public IActionResult SetPassive(int id)
        {
            categoryManager.TSetCategoryPassive(id);
            return RedirectToAction("Category", "Admin", "Index");
        }

    }
}
