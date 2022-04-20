using AutoMapper;
using BookStore_Models.Data;
using BookStore_Models.Responses;

namespace BookStore_API.Services
{
    public interface IStockService
    {
        IEnumerable<AutoListResponse> GetBooks(string searchString);

    }
    public class StockService : IStockService
    {
        private BookStoreDbContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public StockService(
            BookStoreDbContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }
        public IEnumerable<AutoListResponse> GetBooks(string? searchString)
        {
            //Note : you can bind same list from database  
            var ObjList = from b in _context.Books
                          select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                var Name = (from N in ObjList
                            where N.BookName.ToLower().StartsWith(searchString.ToLower())
                            select new AutoListResponse { Id = N.Id, Name = N.BookName });
                return Name;
            }
            else
            {
                var Name = (from N in ObjList
                            select new AutoListResponse { Id = N.Id, Name = N.BookName });
                return Name;
            }
        }


    }
}
