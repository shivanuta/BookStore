using AutoMapper;
using BookStore_API.Authorization;
using BookStore_Models.Data;
using BookStore_Models.DBModels;
using BookStore_Models.Requests;
using BookStore_Models.Responses;

namespace BookStore_API.Services
{
    public interface ICategoriesService
    {
        List<CategoriesResponse> GetAllCategories(string searchString);
        ApiResponseMessage SaveCategory(CategoryRequest categoryRequest);

        CategoryRequest GetCategoryById(int id);
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

            var response = _mapper.Map<List<CategoriesResponse>>(categories.Where(x => x.IsActive).ToList());

            return response;
        }

        public ApiResponseMessage SaveCategory(CategoryRequest categoryRequest)
        {
            ApiResponseMessage response = new ApiResponseMessage();
            // validate
            if (_context.Categories.Any(x => x.CategoryName == categoryRequest.CategoryName))
            {
                response.ErrorMessage = "CategoryName '" + categoryRequest.CategoryName + "' is already taken";
                response.IsSuccess = false;
                return response;
            }

            // map model to new user object
            var category = _mapper.Map<Categories>(categoryRequest);

            // save category
            if (category.Id != 0)
            {
                _context.Categories.Update(category);
            }
            else
            {
                _context.Categories.Add(category);
            }
            _context.SaveChanges();
            response.SuccessMessage = "Category Added successful";
            response.IsSuccess = true;
            return response;
        }

        public CategoryRequest GetCategoryById(int id)
        {
            var categories = from c in _context.Categories
                             select c;

            categories = categories.Where(s => s.Id == id && s.IsActive);

            var response = _mapper.Map<CategoryRequest>(categories.FirstOrDefault());

            return response;
        }
    }
}
