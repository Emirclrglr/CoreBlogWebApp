using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Concrete;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using BusinessLayer.ValidationRules;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using Microsoft.AspNetCore.Identity;

namespace Blog.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        BlogManager blogManager = new BlogManager(new EFBlogDal());

        public IActionResult Index(int page = 1)
        {
            var values = blogManager.TGetListWithCategory().OrderByDescending(x=>x.BlogCreateDate).ToPagedList(page, 9);
            return View(values);
        }

        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = blogManager.TGetBlogByIdWithAuthor(id);
            return View(values);
        }      
        
        public async Task <IActionResult> MyBlogs(int page = 1)
        {
            using var c = new Context();
            var user = User.Identity.Name;
            var mail = c.Users.Where(x => x.UserName == user).Select(y => y.Email).FirstOrDefault();
            var authUserId = c.Authors.Where(x => x.AuthorMail == mail).Select(y => y.AuthorId).FirstOrDefault();
            var values = blogManager.TGetListByAuthorWithCategoryList(authUserId).ToPagedList(page, 10);
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateBlog()
        {
            using Context context = new Context();
            List<SelectListItem> listItems = (from x in context.Categories
                                              select new SelectListItem
                                              {
                                                  Text = x.CategoryName,
                                                  Value = x.CategoryId.ToString()
                                              }).ToList();
            ViewBag.CategoryList = listItems;
            return View();
        }
        [HttpPost]
        public IActionResult CreateBlog(EntityLayer.Concrete.Blog blog)
        {
            using Context context = new Context();
            List<SelectListItem> listItems = (from x in context.Categories
                                              select new SelectListItem
                                              {
                                                  Text = x.CategoryName,
                                                  Value = x.CategoryId.ToString()
                                              }).ToList();

            ViewBag.CategoryList = listItems;

            using var c = new Context();
            var user = User.Identity.Name;
            var mail = c.Users.Where(x=>x.UserName == user).Select(y=>y.Email).FirstOrDefault();
            var authUserId = c.Authors.Where(x => x.AuthorMail == mail).Select(y => y.AuthorId).FirstOrDefault();

            CreateBlogValidator validationRules = new CreateBlogValidator();
            ValidationResult validationResult = validationRules.Validate(blog);
            if (validationResult.IsValid)
            {
                blog.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.BlogStatus = true;
                blog.AuthorId = authUserId;
                blogManager.Insert(blog);
                return RedirectToAction("MyBlogs");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            var getId = blogManager.GetById(id);
            blogManager.Delete(getId);
            return RedirectToAction("MyBlogs");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using Context context = new Context();
            List<SelectListItem> listItems = (from x in context.Categories
                                              select new SelectListItem
                                              {
                                                  Text = x.CategoryName,
                                                  Value = x.CategoryId.ToString()
                                              }).ToList();

            ViewBag.CategoryList = listItems;
            var value = blogManager.GetById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult Edit(EntityLayer.Concrete.Blog blog)
        {
            using Context context = new Context();
            List<SelectListItem> listItems = (from x in context.Categories
                                              select new SelectListItem
                                              {
                                                  Text = x.CategoryName,
                                                  Value = x.CategoryId.ToString()
                                              }).ToList();

            ViewBag.CategoryList = listItems;
            using var c = new Context();
            blogManager.Update(blog);
            return RedirectToAction("MyBlogs");
        }
    }
}
