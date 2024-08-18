using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreBlogWebApp.Controllers
{
    [AllowAnonymous]
    public class ErrorPagesController : Controller
    {
        public IActionResult Error1(int code)
        {
            return View();
        }
    }
}
