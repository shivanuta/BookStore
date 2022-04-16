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
    public class BooksController : ControllerBase
    {
        private IBookService _booksService;

        private IMapper _mapper;

        public BooksController(
            IBookService booksService,
            IMapper mapper)
        {
            _booksService = booksService;
            _mapper = mapper;
        }

        [HttpPost("SaveBook")]
        public IActionResult SaveBook([FromForm]BookRequest bookRequest)
        {
            var response = _booksService.SaveBook(bookRequest);
            return Ok(response);
        }
    }
}
