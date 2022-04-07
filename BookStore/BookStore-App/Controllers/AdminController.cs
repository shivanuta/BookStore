using BookStore_App.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BookStore_App.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}