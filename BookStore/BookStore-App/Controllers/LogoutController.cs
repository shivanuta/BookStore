using BookStore_App.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BookStore_App.Controllers
{
    [Authorize]
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}


