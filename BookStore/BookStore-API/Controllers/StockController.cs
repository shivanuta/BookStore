using AutoMapper;
using BookStore_API.Authorization;
using BookStore_API.Services;
using BookStore_Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
        private IStockService _stockService;

        private IMapper _mapper;

        public StockController(
            IStockService stockService,
            IMapper mapper)
        {
            _stockService = stockService;
            _mapper = mapper;
        }

        [HttpGet("GetBooks/{searchString}")]
        public IActionResult GetBooks(string searchString)
        {
           var response = _stockService.GetBooks(searchString).ToList();
            return Ok(response);
        }

        [HttpPost("UpdateStockDetails")]
        public IActionResult UpdateStockDetails(StockRequest stockRequest)
        {
            var response = _stockService.UpdateStockDetails(stockRequest);
            return Ok(response);
        }
    }
}
