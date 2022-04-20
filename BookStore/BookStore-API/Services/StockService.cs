using AutoMapper;
using BookStore_Models.Data;
using BookStore_Models.DBModels;
using BookStore_Models.Requests;
using BookStore_Models.Responses;

namespace BookStore_API.Services
{
    public interface IStockService
    {
        IEnumerable<AutoListResponse> GetBooks(string searchString);

        ApiResponseMessage UpdateStockDetails(StockRequest stockRequest);

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

        public ApiResponseMessage UpdateStockDetails(StockRequest stockRequest)
        {
            ApiResponseMessage response = new ApiResponseMessage();
            // validate
            if (_context.Stock.Any(x => x.BookId != stockRequest.AutoListResponse.Id))
            {
                response.ErrorMessage = "Book '" + stockRequest.AutoListResponse.Name + "' is Not Exist";
                response.IsSuccess = false;
                return response;
            }

            // map model to new user object
            var stock = _mapper.Map<Stock>(stockRequest);

            // save category
            if (stock.Id != 0)
            {
                stock.AvailableStock = stock.TotalStock;
                _context.Stock.Update(stock);
            }
            else
            {
                stock.AvailableStock = stock.TotalStock;
                _context.Stock.Add(stock);
            }
            _context.SaveChanges();
            response.SuccessMessage = "Stock Added successful";
            response.IsSuccess = true;
            return response;
        }
    }
}
