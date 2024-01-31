using Book4H2Ten.Host.BaseController;
using Book4H2Ten.Services.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System;
using Book4H2Ten.Services.Books.Dtos;

namespace Book4H2Ten.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : WebBaseController
    {
        private readonly IBookService _bookService;

        public BookController (IBookService bookService)
        {
            _bookService = bookService;
        }

        [SwaggerOperation(Summary = "Get book detail")]
        [AllowAnonymous]
        [HttpGet("{bookId}")]
        public async Task<BookDtos> GetBookAsync(Guid bookId)
          => await _bookService.GetBookAsync(bookId);

        [SwaggerOperation(Summary = "Create book")]
        [AllowAnonymous]
        [HttpPost("{typeBookId}")]
        public async Task<BookDtos> CreateBook(BookDtos bookDtos, Guid typeBookId)
            => await _bookService.CreateBook(bookDtos, typeBookId);

        [SwaggerOperation(Summary = "Edit book")]
        [AllowAnonymous]
        [HttpPatch("{bookId}")]
        public async Task<BookDtos> EditBook(BookDtos bookDtos, Guid bookId)
            => await _bookService.EditBook(bookDtos, bookId);

        [SwaggerOperation(Summary = "Delete book")]
        [AllowAnonymous]
        [HttpDelete("{bookId}")]
        public async Task DeleteBook(Guid bookId) 
            => await _bookService.DeleteBook(bookId);
    }
}
