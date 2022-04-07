using BookStore_App.Authorization;
using BookStore_Models.Requests;
using BookStore_Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookStore_App.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _Configure;
        private static string apiBaseUrl;

        public LoginController(ILogger<LoginController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configure = configuration;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        //Admin Login View
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Method to authenticate Admin login   
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AdminLoginCheck(AuthenticateRequest user)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "Users/adminAuthenticate";

                using (var Response = await client.PostAsync(endpoint, content))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var authenticateResponse = JsonConvert.DeserializeObject<AdminAuthenticateResponse>(apiResponse);

                    if (authenticateResponse != null && authenticateResponse.ResponseMesssage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContext.Session.SetString("Token", authenticateResponse.Token);
                        HttpContext.Session.SetInt32("IsAdmin", 1);
                        HttpContext.Session.SetString("Username", authenticateResponse.Username);
                        HttpContext.Session.SetString("Name", authenticateResponse.FirstName + " " + authenticateResponse.LastName);
                        TempData["Profile"] = JsonConvert.SerializeObject(apiResponse);
                        return RedirectToAction("Index", "Profile");
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Username or Password is Incorrect");
                        return View("Index");
                    }
                }
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }
        // Method to authenticate user login   
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UserLoginCheck(AuthenticateRequest user)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "Users/authenticate";

                using (var Response = await client.PostAsync(endpoint, content))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var authenticateResponse = JsonConvert.DeserializeObject<AuthenticateResponse>(apiResponse);

                    if (authenticateResponse != null && authenticateResponse.ResponseMesssage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContext.Session.SetString("Token", authenticateResponse.Token);
                        HttpContext.Session.SetInt32("IsAdmin", 0);
                        HttpContext.Session.SetString("Username", authenticateResponse.Username);
                        HttpContext.Session.SetString("Name", authenticateResponse.FirstName + " " + authenticateResponse.LastName);
                        TempData["Profile"] = JsonConvert.SerializeObject(apiResponse);
                        return RedirectToAction("Index", "Profile");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Username or Password is Incorrect");
                        return View("UserLogin");
                    }
                }
            }
        }


    }
}
