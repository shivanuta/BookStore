using BookStore_App.Authorization;
using BookStore_Models.Requests;
using BookStore_Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookStore_App.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IConfiguration _Configure;
        private static string apiBaseUrl;

        public CategoriesController(ILogger<CategoriesController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configure = configuration;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }
        public async Task<IActionResult> Index()
        {
            var categoriesList = await GetAllCategories(String.Empty);
            return View(categoriesList);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await GetCategory(id);

            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await GetCategory(id);

            return View("Create", category);
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryRequest categoryRequest)
        {
            if (categoryRequest.Id != 0)
            {
                categoryRequest.ModifiedBy = HttpContext.Session.GetInt32("UserId");
                categoryRequest.ModifiedDate = System.DateTime.UtcNow;
            }
            else
            {
                categoryRequest.CreatedBy = HttpContext.Session.GetInt32("UserId");
                categoryRequest.CreatedDate = System.DateTime.UtcNow;
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(categoryRequest), Encoding.UTF8, "application/json");
            var token = HttpContext.Session.GetString("Token");
            string endpoint = apiBaseUrl + "Categories/CreateCategory";

            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", token);
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<ApiResponseMessage>(apiResponse);

                    if (responseMessage != null && responseMessage.IsSuccess && Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var categoriesList = await GetAllCategories(String.Empty);
                        return View("Index", categoriesList);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, responseMessage.ErrorMessage);
                        return View("Create", categoryRequest);
                    }
                }
            }



        }

        private async Task<List<CategoriesResponse>> GetAllCategories(string searchString)
        {
            using (HttpClient client = new HttpClient())
            {
                var token = HttpContext.Session.GetString("Token");
                string endpoint = apiBaseUrl + "Categories/GetCategories/" + searchString;
                client.DefaultRequestHeaders.Add("Authorization", token);
                using (var Response = await client.GetAsync(endpoint))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<List<CategoriesResponse>>(apiResponse);

                    if (responseMessage != null && Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return responseMessage;
                    }
                    else
                    {
                        return new List<CategoriesResponse>();
                    }
                }
            }
        }

        private async Task<CategoryRequest> GetCategory(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var token = HttpContext.Session.GetString("Token");
                string endpoint = apiBaseUrl + "Categories/GetCategoryById/" + id;
                client.DefaultRequestHeaders.Add("Authorization", token);
                using (var Response = await client.GetAsync(endpoint))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<CategoryRequest>(apiResponse);

                    if (responseMessage != null && Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return responseMessage;
                    }
                    else
                    {
                        return new CategoryRequest();
                    }
                }
            }
        }
    }
}
