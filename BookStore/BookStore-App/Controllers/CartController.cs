using BookStore_APP.Helper;
using BookStore_Models.Requests;
using BookStore_Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore_App.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly IConfiguration _Configure;
        private static string apiBaseUrl;

        public CartController(ILogger<CartController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configure = configuration;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItems>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if (cart != null)
            {
                ViewBag.total = cart.Sum(item => (item.BooksDetailsResponse.ActualPrice - (item.BooksDetailsResponse.ActualPrice * item.BooksDetailsResponse.DiscountPercentage / 100)) * item.Quantity);
            }
            return View();
        }

        [Route("AddToCart/{id}")]
        public async Task<IActionResult> AddToCart(int id)
        {

            List<BooksDetailsResponse> productModel = await GetAllBooksDetails(string.Empty);
            if (SessionHelper.GetObjectFromJson<List<CartItems>>(HttpContext.Session, "cart") == null)
            {
                List<CartItems> cart = new List<CartItems>();
                cart.Add(new CartItems { BooksDetailsResponse = productModel.First(x => x.Id == id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItems> cart = SessionHelper.GetObjectFromJson<List<CartItems>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItems { BooksDetailsResponse = productModel.First(x => x.Id == id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<CartItems> cart = SessionHelper.GetObjectFromJson<List<CartItems>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<CartItems> cart = SessionHelper.GetObjectFromJson<List<CartItems>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].BooksDetailsResponse.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        private async Task<List<BooksDetailsResponse>> GetAllBooksDetails(string searchString)
        {
            using (HttpClient client = new HttpClient())
            {
                var token = HttpContext.Session.GetString("Token");
                string endpoint = apiBaseUrl + "Shop/GetBooksDetails/" + searchString;
                using (var Response = await client.GetAsync(endpoint))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<List<BooksDetailsResponse>>(apiResponse);

                    if (responseMessage != null && Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return responseMessage;
                    }
                    else
                    {
                        return new List<BooksDetailsResponse>();
                    }
                }
            }
        }
    }
}
