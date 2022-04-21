using AutoMapper;
using BookStore_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private IShopService _shopService;

        private IMapper _mapper;

        public ShopController(
            IShopService shopService,
            IMapper mapper)
        {
            _shopService = shopService;
            _mapper = mapper;
        }

        [HttpGet("GetBooksDetails/{searchString?}")]
        public IActionResult GetBooksDetails(string? searchString = null)
        {
            var response = _shopService.GetAllBooksDetails(searchString);
            return Ok(response);
        }
    }
}
