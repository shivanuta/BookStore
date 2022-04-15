using AutoMapper;
using BookStore_API.Authorization;
using BookStore_API.Services;
using BookStore_Models.Requests;
using BookStore_Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private ICategoriesService _categoriesService;

        private IMapper _mapper;

        public CategoriesController(
            ICategoriesService categoriesService,
            IMapper mapper)
        {
            _categoriesService = categoriesService;
            _mapper = mapper;
        }

        [HttpGet("GetCategories/{searchString?}")]
        public IActionResult GetCategories(string? searchString = null)
        {
            var response = _categoriesService.GetAllCategories(searchString).ToList();
            return Ok(response);
        }

        [HttpPost("CreateCategory")]
        public IActionResult CreateCategory(CategoryRequest categoryRequest)
        {
            var response = _categoriesService.SaveCategory(categoryRequest);
            return Ok(response);
        }
    }
}
