using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookStore_Models.Responses;
using BookStore_API.Services;
using AutoMapper;
using BookStore_API.Helper;
using Microsoft.Extensions.Options;
using BookStore_API.Authorization;
using BookStore_Models.Requests;

namespace BookStore_API.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            string response = _userService.Register(model);
            return Ok(new { message = response });
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }
    }
}
