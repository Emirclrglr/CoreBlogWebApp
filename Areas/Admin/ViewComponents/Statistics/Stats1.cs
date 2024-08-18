using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace CoreBlogWebApp.Areas.Admin.ViewComponents.Statistics
{
    public class Stats1:ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogDal());
        Context context = new Context();
        public IViewComponentResult Invoke()
        {
            ViewBag.BlogCount = blogManager.BlogCount();
            ViewBag.MessageCount = context.Contacts.Count();
            ViewBag.CommentCount = context.Comments.Count();

            string api = "75cd097d265939c430cb300676dd28b7";
            string connection = $"https://api.openweathermap.org/data/2.5/weather?q=Istanbul&mode=xml&lang=tr&units=metric&appid={api}";
            XDocument document = XDocument.Load(connection);
            ViewBag.Temperature = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;

            return View();
        }
    }
}
