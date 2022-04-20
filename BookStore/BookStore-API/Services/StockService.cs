using AutoMapper;
using BookStore_Models.Data;
using BookStore_Models.DBModels;
using BookStore_Models.Requests;
using BookStore_Models.Responses;
using Microsoft.EntityFrameworkCore;

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
            
            var stock = _mapper.Map<Stock>(stockRequest);

            var stockObj = GetStockByBookId(stock.BookId);

            if (stockObj != null)
            {
                stock.Id = stockObj.Id;
                stock.AvailableStock = stock.TotalStock;
                _context.Stock.Update(stock);
            }
            else
            {
                stock.AvailableStock = stock.TotalStock;
                _context.Stock.Add(stock);
            }
            _context.SaveChanges();
            response.SuccessMessage = "Stock Added successfully";
            response.IsSuccess = true;
            return response;
        }

        private StockRequest GetStockByBookId(int bookId)
        {
            var stockDetails = from c in _context.Stock
                             select c;

            stockDetails = stockDetails.Where(s => s.BookId == bookId);

            var response = _mapper.Map<StockRequest>(stockDetails.AsNoTracking().FirstOrDefault());

            return response;
        }
    }
}
