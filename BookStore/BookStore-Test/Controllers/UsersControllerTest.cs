using BookStore_API.Controllers;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using BookStore_API.Services;
using AutoMapper;
using BookStore_Models.Requests;
using Microsoft.AspNetCore.Http;

namespace BookStore_Test.Controllers
{
    public class UsersControllerTest
    {
        [Fact]
        public void UserController_AdminLoginSuccess_Test()
        {
            var mockUserService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new UsersController(mockUserService.Object, mockMapper.Object);

            var authenticateRequest = new AuthenticateRequest
            {
                Username = "AdminBookStore1",
                Password = "123123"
            };
            var result = controller.AdminAuthenticate(authenticateRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void UserController_AdminLogin_MissingUserneme_Test()
        {
            var mockUserService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new UsersController(mockUserService.Object, mockMapper.Object);

            var authenticateRequest = new AuthenticateRequest
            {
                Password = "123123"
            };
            var result = controller.AdminAuthenticate(authenticateRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
        [Fact]
        public void UserController_AdminLogin_MissingPassword_Test()
        {
            var mockUserService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new UsersController(mockUserService.Object, mockMapper.Object);

            var authenticateRequest = new AuthenticateRequest
            {
                Username = "AdminBookStore1",
            };
            var result = controller.AdminAuthenticate(authenticateRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
        [Fact]
        public void UserController_AdminLogin_EmptyUsernamePassword_Test()
        {
            var mockUserService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new UsersController(mockUserService.Object, mockMapper.Object);

            var authenticateRequest = new AuthenticateRequest();

            var result = controller.AdminAuthenticate(authenticateRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
        [Fact]
        public void UserController_AdminLogin_InvalidCredentials_Test()
        {
            var mockUserService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new UsersController(mockUserService.Object, mockMapper.Object);

            var authenticateRequest = new AuthenticateRequest
            {
                Username = "test",
                Password = "test"
            };
            var result = controller.AdminAuthenticate(authenticateRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void UserController_UserLoginSuccess_Test()
        {
            var mockUserService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new UsersController(mockUserService.Object, mockMapper.Object);

            var authenticateRequest = new AuthenticateRequest
            {
                Username = "shiva",
                Password = "123123"
            };
            var result = controller.Authenticate(authenticateRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void UserController_UserLogin_MissingUserneme_Test()
        {
            var mockUserService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new UsersController(mockUserService.Object, mockMapper.Object);

            var authenticateRequest = new AuthenticateRequest
            {
                Password = "123123"
            };
            var result = controller.Authenticate(authenticateRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
        [Fact]
        public void UserController_UserLogin_MissingPassword_Test()
        {
            var mockUserService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new UsersController(mockUserService.Object, mockMapper.Object);

            var authenticateRequest = new AuthenticateRequest
            {
                Username = "shiva",
            };
            var result = controller.Authenticate(authenticateRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
        [Fact]
        public void UserController_UserLogin_EmptyUsernamePassword_Test()
        {
            var mockUserService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new UsersController(mockUserService.Object, mockMapper.Object);

            var authenticateRequest = new AuthenticateRequest();

            var result = controller.Authenticate(authenticateRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
        [Fact]
        public void UserController_UserLogin_InvalidCredentials_Test()
        {
            var mockUserService = new Mock<IUserService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new UsersController(mockUserService.Object, mockMapper.Object);

            var authenticateRequest = new AuthenticateRequest
            {
                Username = "test",
                Password = "test"
            };
            var result = controller.Authenticate(authenticateRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
    }
}
