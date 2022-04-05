using BookStore_Models.Responses;
using BookStore_Models.Data;
using BookStore_API.Helper;
using BookStore_API.Authorization;
using AutoMapper;
using BookStore_Models.DBModels;
using System.Net;
using BookStore_Models.Requests;

namespace BookStore_API.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Users GetById(int id);
    }
    public class UserService : IUserService
    {
        private BookStoreDbContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(
            BookStoreDbContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                var authenticateResponse = new AuthenticateResponse()
                {
                    ErrorMessage = "Username or password is incorrect",
                    ResponseMesssage = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                };
                return authenticateResponse;
            }

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            response.ResponseMesssage = new HttpResponseMessage(HttpStatusCode.OK);
            return response;
        }

        public Users GetById(int id)
        {
            return getUser(id);
        }

        private Users getUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

    }
}
