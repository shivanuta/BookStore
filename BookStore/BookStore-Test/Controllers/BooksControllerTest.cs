using BookStore_API.Controllers;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using BookStore_API.Services;
using AutoMapper;
using BookStore_Models.Requests;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.IO;

namespace BookStore_Test.Controllers
{
    public class BooksControllerTest
    {
        [Fact]
        public void BooksController_GetBooksSearchString_NotSending_Test()
        {
            var mockBookService = new Mock<IBookService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new BooksController(mockBookService.Object, mockMapper.Object);
            var result = controller.GetBooks();
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void BooksController_GetBooksSearchString_Null_Test()
        {
            var mockBookService = new Mock<IBookService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new BooksController(mockBookService.Object, mockMapper.Object);
            string searchString = null;
            var result = controller.GetBooks(searchString);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void BooksController_GetBooksSearchString_EmptyString_Test()
        {
            var mockBookService = new Mock<IBookService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new BooksController(mockBookService.Object, mockMapper.Object);
            string searchString = string.Empty;
            var result = controller.GetBooks(searchString);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void BooksController_GetBooksSearchString_Success_Test()
        {
            var mockBookService = new Mock<IBookService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new BooksController(mockBookService.Object, mockMapper.Object);
            string searchString = "Novels";
            var result = controller.GetBooks(searchString);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void BooksController_DeleteBook_Success_Test()
        {
            var mockBookService = new Mock<IBookService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new BooksController(mockBookService.Object, mockMapper.Object);
            int id = 1;
            var result = controller.DeleteBook(id);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void BooksController_DeleteBook_Failure_Test()
        {
            var mockBookService = new Mock<IBookService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new BooksController(mockBookService.Object, mockMapper.Object);
            int id = 0;
            var result = controller.DeleteBook(id);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void BooksController_GetBookById_Success_Test()
        {
            var mockBookService = new Mock<IBookService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new BooksController(mockBookService.Object, mockMapper.Object);
            int id = 1;
            var result = controller.GetBookById(id);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void BooksController_GetBookById_0_Test()
        {
            var mockBookService = new Mock<IBookService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new BooksController(mockBookService.Object, mockMapper.Object);
            int id = 0;
            var result = controller.GetBookById(id);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void BooksController_SaveBook_Success_Test()
        {
            var mockBookService = new Mock<IBookService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new BooksController(mockBookService.Object, mockMapper.Object);
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt");
            BookRequest bookRequest = new BookRequest
            {
                Id = 0,
                BookName = "test book",
                Author = "test",
                BookImage = file,
                BookTitle = "test title",
                CategoryId = 1,
                Publisher = "test publisher",
                Published = "4/22/2022",
                IsActive = true,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
            var result = controller.SaveBook(bookRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void BooksController_SaveBook_EmptyObject_Test()
        {
            var mockBookService = new Mock<IBookService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new BooksController(mockBookService.Object, mockMapper.Object);
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt");
            BookRequest bookRequest = new BookRequest();
            var result = controller.SaveBook(bookRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void BooksController_SaveBook_MissedProps_Test()
        {
            var mockBookService = new Mock<IBookService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new BooksController(mockBookService.Object, mockMapper.Object);
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt");
            BookRequest bookRequest = new BookRequest
            {
                Id = 0,
                BookName = "test book",
                Author = "test",
                BookImage = file,
                CreatedBy = 1,
                CreatedDate = System.DateTime.Now
            };
            var result = controller.SaveBook(bookRequest);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
    }
}
