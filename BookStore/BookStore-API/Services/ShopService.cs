using AutoMapper;
using BookStore_Models.Data;
using BookStore_Models.Responses;

namespace BookStore_API.Services
{
    public interface IShopService
    {
        List<BooksDetailsResponse> GetAllBooksDetails(string searchString);
    }
    public class ShopService : IShopService
    {
        private BookStoreDbContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ShopService(
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

        public List<BooksDetailsResponse> GetAllBooksDetails(string searchString)
        {
            var booksDetails =   from bk in _context.Books
                                        join st in _context.Stock on bk.Id equals st.BookId
                                        select new BooksDetailsResponse
                                        {
                                            Id = bk.Id,
                                            BookTitle = bk.BookName,
                                            BookImage = bk.BookImage,
                                            Author = bk.Author,
                                            PublishedDate = bk.Published,
                                            ActualPrice = st.AmountPerBook,
                                            DiscountPercentage = st.DiscountPercentage
                                        };

            if (!String.IsNullOrEmpty(searchString))
            {
                return booksDetails.Where(p => p.BookTitle.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return booksDetails.ToList();
        }
    }
}
