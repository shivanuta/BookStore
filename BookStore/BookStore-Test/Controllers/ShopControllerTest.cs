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
    public class ShopControllerTest
    {
        [Fact]
        public void ShopController_GetBooksSearchString_NotSending_Test()
        {
            var mockShopService = new Mock<IShopService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new ShopController(mockShopService.Object, mockMapper.Object);
            var result = controller.GetBooksDetails();
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void ShopController_GetBooksSearchString_Null_Test()
        {
            var mockShopService = new Mock<IShopService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new ShopController(mockShopService.Object, mockMapper.Object);
            string searchString = null;
            var result = controller.GetBooksDetails(searchString);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void ShopController_GetBooksSearchString_EmptyString_Test()
        {
            var mockShopService = new Mock<IShopService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new ShopController(mockShopService.Object, mockMapper.Object);
            string searchString = string.Empty;
            var result = controller.GetBooksDetails(searchString);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void ShopController_GetBooksSearchString_Success_Test()
        {
            var mockShopService = new Mock<IShopService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new ShopController(mockShopService.Object, mockMapper.Object);
            string searchString = "Jurrasic";
            var result = controller.GetBooksDetails(searchString);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
    }
}
