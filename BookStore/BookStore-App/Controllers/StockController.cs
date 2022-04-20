using BookStore_Models.Requests;
using BookStore_Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookStore_App.Controllers
{
    public class StockController : Controller
    {
        private readonly ILogger<StockController> _logger;
        private readonly IConfiguration _Configure;
        private static string apiBaseUrl;

        public StockController(ILogger<StockController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configure = configuration;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string Prefix)
        {
            List<AutoListResponse> booksList = await GetAllBooks(Prefix);
            StockRequest stockRequest = new StockRequest
            {
                booksList = booksList
            };
            return Ok(stockRequest);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStock(StockRequest stockRequest)
        {
            if (stockRequest.Id != 0)
            {
                stockRequest.ModifiedBy = HttpContext.Session.GetInt32("UserId");
                stockRequest.ModifiedDate = System.DateTime.UtcNow;
            }
            else
            {
                stockRequest.CreatedBy = HttpContext.Session.GetInt32("UserId");
                stockRequest.CreatedDate = System.DateTime.UtcNow;
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(stockRequest), Encoding.UTF8, "application/json");
            var token = HttpContext.Session.GetString("Token");
            string endpoint = apiBaseUrl + "Stock/UpdateStockDetails";

            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", token);
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<ApiResponseMessage>(apiResponse);

                    if (responseMessage != null && responseMessage.IsSuccess && Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ModelState.Clear();
                        TempData["Message"] = responseMessage.SuccessMessage;
                        return View("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, responseMessage.ErrorMessage);
                        return View("Index", stockRequest);
                    }
                }
            }
        }

        private async Task<List<AutoListResponse>> GetAllBooks(string searchString)
        {
            using (HttpClient client = new HttpClient())
            {
                var token = HttpContext.Session.GetString("Token");
                string endpoint = apiBaseUrl + "Stock/GetBooks/" + searchString;
                client.DefaultRequestHeaders.Add("Authorization", token);
                using (var Response = await client.GetAsync(endpoint))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<List<AutoListResponse>>(apiResponse);

                    if (responseMessage != null && Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return responseMessage;
                    }
                    else
                    {
                        return new List<AutoListResponse>();
                    }
                }
            }
        }

    }
}
