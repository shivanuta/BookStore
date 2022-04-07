using BookStore_App.Authorization;
using BookStore_Models.Requests;
using BookStore_Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookStore_App.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly IConfiguration _Configure;
        private static string apiBaseUrl;

        public RegistrationController(ILogger<RegistrationController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configure = configuration;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UserRegistration(RegisterRequest user)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "Users/register";

                using (var Response = await client.PostAsync(endpoint, content))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<ApiResponseMessage>(apiResponse);

                    if (responseMessage != null && responseMessage.IsSuccess && Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ModelState.Clear();
                        TempData["Message"] = responseMessage.SuccessMessage;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, responseMessage.ErrorMessage);
                        return View("Index");
                    }
                }
            }
        }

    }
}
