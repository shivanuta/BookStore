using BookStore_App.Authorization;
using BookStore_Models.Requests;
using BookStore_Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookStore_App.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IConfiguration _Configure;
        private static string apiBaseUrl;

        public BooksController(ILogger<BooksController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configure = configuration;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var categoriesList = await GetAllCategories(String.Empty);
            ViewBag.categories = categoriesList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookRequest bookRequest)
        {
            var multipartContent = new MultipartFormDataContent();

            if (bookRequest.Id != 0)
            {
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(HttpContext.Session.GetInt32("UserId"))), "ModifiedBy");
            }
            else
            {
                multipartContent.Add(new StringContent(JsonConvert.SerializeObject(HttpContext.Session.GetInt32("UserId"))), "CreatedBy");
            }

            multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookRequest.Id)), "Id");
            multipartContent.Add(new StringContent(bookRequest.Author), "Author");
            multipartContent.Add(new StringContent(JsonConvert.SerializeObject(bookRequest.CategoryId)), "CategoryId");
            multipartContent.Add(new StringContent(bookRequest.BookTitle), "BookTitle");
            multipartContent.Add(new StringContent(bookRequest.BookName), "BookName");
            multipartContent.Add(new StringContent(bookRequest.Publisher), "Publisher");
            multipartContent.Add(new StringContent(bookRequest.Published), "Published");


            multipartContent.Add(new StreamContent(bookRequest.BookImage.OpenReadStream()), "BookImage", bookRequest.BookImage.FileName);


            //var response = await client.PostAsync("url_here", multipartContent);

            //StringContent content = new StringContent(JsonConvert.SerializeObject(bookRequest), Encoding.UTF8, "application/json");
            var token = HttpContext.Session.GetString("Token");
            string endpoint = apiBaseUrl + "Books/SaveBook";

            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", token);
                //using (var Response = await client.PostAsync(endpoint, content))
                using (var Response = await client.PostAsync(endpoint, multipartContent))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<ApiResponseMessage>(apiResponse);

                    if (responseMessage != null && responseMessage.IsSuccess && Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //var categoriesList = await GetAllCategories(String.Empty);
                        //return View("Index", categoriesList);
                        return View("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, responseMessage.ErrorMessage);
                        return View("Create", bookRequest);
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


    }
}
