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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(ILogger<BooksController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _Configure = configuration;
            _webHostEnvironment = webHostEnvironment;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }
        public async Task<IActionResult> Index()
        {
            var BookssList = await GetAllBooks(String.Empty);
            return View(BookssList);
        }

        public async Task<IActionResult> Create()
        {
            var categoriesList = await GetAllCategories(String.Empty);
            ViewBag.categories = categoriesList;
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await GetBook(id);

            return View(book);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoriesList = await GetAllCategories(String.Empty);
            ViewBag.categories = categoriesList;

            var book = await GetBook(id);
            var bookRequest = GetFile(book.BookImageName, book);

            return View("Create", bookRequest);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("Token");
            string endpoint = apiBaseUrl + "Books/DeleteBook/" + id;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", token);
                using (var Response = await client.GetAsync(endpoint))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<ApiResponseMessage>(apiResponse);
                    var booksList = await GetAllBooks(String.Empty);
                    return View("Index", booksList);
                }
            }
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


            var token = HttpContext.Session.GetString("Token");
            string endpoint = apiBaseUrl + "Books/SaveBook";

            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", token);
                using (var Response = await client.PostAsync(endpoint, multipartContent))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<ApiResponseMessage>(apiResponse);

                    if (responseMessage != null && responseMessage.IsSuccess && Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var BooksList = await GetAllBooks(String.Empty);
                        UploadedFile(responseMessage, bookRequest);
                        return View("Index", BooksList);
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

        private async Task<List<BooksResponse>> GetAllBooks(string searchString)
        {
            using (HttpClient client = new HttpClient())
            {
                var token = HttpContext.Session.GetString("Token");
                string endpoint = apiBaseUrl + "Books/GetBooks/" + searchString;
                client.DefaultRequestHeaders.Add("Authorization", token);
                using (var Response = await client.GetAsync(endpoint))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<List<BooksResponse>>(apiResponse);

                    if (responseMessage != null && Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return responseMessage;
                    }
                    else
                    {
                        return new List<BooksResponse>();
                    }
                }
            }
        }


        private string UploadedFile(ApiResponseMessage response, BookRequest model)
        {
            string uniqueFileName = null;

            if (response.UniqueImageName != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/Books");
                uniqueFileName = response.UniqueImageName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.BookImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        private async Task<BookRequest> GetBook(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var token = HttpContext.Session.GetString("Token");
                string endpoint = apiBaseUrl + "Books/GetBookById/" + id;
                client.DefaultRequestHeaders.Add("Authorization", token);
                using (var Response = await client.GetAsync(endpoint))
                {
                    var apiResponse = await Response.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<BookRequest>(apiResponse);

                    if (responseMessage != null && Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return responseMessage;
                    }
                    else
                    {
                        return new BookRequest();
                    }
                }
            }
        }

        public BookRequest GetFile(string? fileName, BookRequest bookRequest)
        {
            using (var stream = System.IO.File.OpenRead(@"./wwwroot/images/books/" + fileName))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(@"./wwwroot/images/books/" + fileName))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };
                bookRequest.BookImage = file;

            }
            return bookRequest;
            //if (!String.IsNullOrEmpty(fileName))
            //{
            //    var filepath = "./wwwroot/images/books/" + fileName;

            //    using (var stream = System.IO.File.OpenRead($"{filepath}"))
            //    {

            //        bookRequest.BookImage = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

            //        // ...
            //        // code logic here
            //    }
            //}
            //return bookRequest;
            //if(!String.IsNullOrEmpty(fileName))
            //{
            //    string path = "./wwwroot/images/books/" + fileName;
            //    using (var stream = System.IO.File.OpenRead(path))
            //    {
            //        return new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
            //    }
            //}
            //else
            //{
            //    return null;
            //}

        }
    }
}
