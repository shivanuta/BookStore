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
    public class CategoriesControllerTest
    {
        [Fact]
        public void CategoriesController_GetCategoriesSearchString_NotSending_Test()
        {
            var mockCatogoryService = new Mock<ICategoriesService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new CategoriesController(mockCatogoryService.Object, mockMapper.Object);
            var result = controller.GetCategories();
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void CategoriesController_GetCategoriesSearchString_Null_Test()
        {
            var mockCatogoryService = new Mock<ICategoriesService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new CategoriesController(mockCatogoryService.Object, mockMapper.Object);
            string searchString = null;
            var result = controller.GetCategories(searchString);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void CategoriesController_GetCategoriesSearchString_EmptyString_Test()
        {
            var mockCatogoryService = new Mock<ICategoriesService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new CategoriesController(mockCatogoryService.Object, mockMapper.Object);
            string searchString = string.Empty;
            var result = controller.GetCategories(searchString);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void CategoriesController_GetCategoriesSearchString_Success_Test()
        {
            var mockCatogoryService = new Mock<ICategoriesService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new CategoriesController(mockCatogoryService.Object, mockMapper.Object);
            string searchString = "Novels";
            var result = controller.GetCategories(searchString);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void CategoriesController_GetCategoryById_Success_Test()
        {
            var mockCatogoryService = new Mock<ICategoriesService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new CategoriesController(mockCatogoryService.Object, mockMapper.Object);
            int id = 1;
            var result = controller.GetCategoryById(id);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void CategoriesController_GetCategoryById_0_Test()
        {
            var mockCatogoryService = new Mock<ICategoriesService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new CategoriesController(mockCatogoryService.Object, mockMapper.Object);
            int id = 0;
            var result = controller.GetCategoryById(id);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void CategoriesController_DeleteCategory_Success_Test()
        {
            var mockCatogoryService = new Mock<ICategoriesService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new CategoriesController(mockCatogoryService.Object, mockMapper.Object);
            int id = 1;
            var result = controller.DeleteCategory(id);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void CategoriesController_DeleteCategory_0Failure_Test()
        {
            var mockCatogoryService = new Mock<ICategoriesService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new CategoriesController(mockCatogoryService.Object, mockMapper.Object);
            int id = 0;
            var result = controller.DeleteCategory(id);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void CategoriesController_CreateCategory_Success_Test()
        {
            var mockCatogoryService = new Mock<ICategoriesService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new CategoriesController(mockCatogoryService.Object, mockMapper.Object);
            CategoryRequest categoryRequest = new CategoryRequest
            {
                Id = 0,
                CategoryName = "Movies",
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
            var result = controller.CreateCategory(categoryRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void CategoriesController_CreateCategory_MissingProperties_Test()
        {
            var mockCatogoryService = new Mock<ICategoriesService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new CategoriesController(mockCatogoryService.Object, mockMapper.Object);
            CategoryRequest categoryRequest = new CategoryRequest
            {
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
            var result = controller.CreateCategory(categoryRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void CategoriesController_CreateCategory_EmptyObject_Test()
        {
            var mockCatogoryService = new Mock<ICategoriesService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new CategoriesController(mockCatogoryService.Object, mockMapper.Object);
            CategoryRequest categoryRequest = new CategoryRequest();
            var result = controller.CreateCategory(categoryRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
    }
}
