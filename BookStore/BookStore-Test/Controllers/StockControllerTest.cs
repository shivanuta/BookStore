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
    public class StockControllerTest
    {
        [Fact]
        public void StockController_UpdateStock_Success_Test()
        {
            var mockStockService = new Mock<IStockService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new StockController(mockStockService.Object, mockMapper.Object);
            StockRequest stockRequest = new StockRequest
            {
                Id = 1,
                AmountPerBook = 100,
                AutoListResponse = new BookStore_Models.Responses.AutoListResponse { Id = 1, Name = "The Jurrasic Park"},
                TotalStock = 100,
                DiscountPercentage = 10,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };

            var result = controller.UpdateStockDetails(stockRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void StockController_UpdateStock_EmptyObject_Test()
        {
            var mockStockService = new Mock<IStockService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new StockController(mockStockService.Object, mockMapper.Object);
            StockRequest stockRequest = new StockRequest();

            var result = controller.UpdateStockDetails(stockRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void StockController_UpdateStock_NullAutoListResponse_Test()
        {
            var mockStockService = new Mock<IStockService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new StockController(mockStockService.Object, mockMapper.Object);
            StockRequest stockRequest = new StockRequest
            {
                Id = 1,
                AmountPerBook = 100,
                AutoListResponse = null,
                TotalStock = 100,
                DiscountPercentage = 10,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };

            var result = controller.UpdateStockDetails(stockRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void StockController_UpdateStock_EmptyAutoListResponse_Test()
        {
            var mockStockService = new Mock<IStockService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new StockController(mockStockService.Object, mockMapper.Object);
            StockRequest stockRequest = new StockRequest
            {
                Id = 1,
                AmountPerBook = 100,
                AutoListResponse = new BookStore_Models.Responses.AutoListResponse(),
                TotalStock = 100,
                DiscountPercentage = 10,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };

            var result = controller.UpdateStockDetails(stockRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void StockController_GetBooks_Autolist_SearchString_Success_Test()
        {
            var mockStockService = new Mock<IStockService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new StockController(mockStockService.Object, mockMapper.Object);
            string searchString = "Jurrasic";
            var result = controller.GetBooks(searchString);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void StockController_GetBooks_Autolist_SearchString_Null_Test()
        {
            var mockStockService = new Mock<IStockService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new StockController(mockStockService.Object, mockMapper.Object);
            string searchString = null;
            var result = controller.GetBooks(searchString);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void StockController_GetBooks_Autolist_SearchString_EmptyString_Test()
        {
            var mockStockService = new Mock<IStockService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new StockController(mockStockService.Object, mockMapper.Object);
            string searchString = "";
            var result = controller.GetBooks(searchString);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
    }
}
