using BookStore_Models.Responses;
using BookStore_Models.Requests;
using BookStore_Models.Data;
using BookStore_API.Authorization;
using AutoMapper;
using BookStore_Models.DBModels;

namespace BookStore_API.Services
{
    public interface IBookService
    {
        ApiResponseMessage SaveBook(BookRequest bookRequest);
    }
    public class BookService : IBookService
    {
        private BookStoreDbContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookService(
            BookStoreDbContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public ApiResponseMessage SaveBook(BookRequest bookRequest)
        {
            ApiResponseMessage response = new ApiResponseMessage();
            // validate
            if (_context.Books.Any(x => x.BookName == bookRequest.BookName && x.Author == bookRequest.Author && x.IsActive == true))
            {
                response.ErrorMessage = "BookName '" + bookRequest.BookName + "' is already exists";
                response.IsSuccess = false;
                return response;
            }

            // map model to new user object
            var book = _mapper.Map<Books>(bookRequest);

            // save category
            if (book.Id != 0)
            {
                var uniqueImageName = UploadedFile(bookRequest);
                book.BookImage = uniqueImageName;
                book.ModifiedDate = book.ModifiedDate != null ? book.ModifiedDate : DateTime.UtcNow;
                _context.Books.Update(book);
            }
            else
            {
                var uniqueImageName = UploadedFile(bookRequest);
                book.BookImage = uniqueImageName;
                book.CreatedDate = book.CreatedDate != null ? book.CreatedDate : DateTime.UtcNow;
                _context.Books.Add(book);
            }
            _context.SaveChanges();
            response.SuccessMessage = "Book Added successful";
            response.IsSuccess = true;
            return response;
        }

        private string UploadedFile(BookRequest model)
        {
            string uniqueFileName = null;

            if (model.BookImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.BookImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.BookImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
