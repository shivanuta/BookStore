using AutoMapper;
using BookStore_API.Authorization;
using BookStore_Models.Data;
using BookStore_Models.DBModels;
using BookStore_Models.Responses;

namespace BookStore_API.Services
{
    public interface ICategoriesService
    {
        List<CategoriesResponse> GetAllCategories(string searchString);
    }
    public class CategoriesService : ICategoriesService
    {
        private BookStoreDbContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public CategoriesService(
            BookStoreDbContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }
        public List<CategoriesResponse> GetAllCategories(string searchString)
        {
            var categories = from c in _context.Categories
                         select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(s => s.CategoryName!.Contains(searchString));
            }

            var response = _mapper.Map<List<CategoriesResponse>>(categories.ToList());

            return response;
        }
    }
}
