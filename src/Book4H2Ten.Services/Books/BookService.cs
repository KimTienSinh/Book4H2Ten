using Book4H2Ten.Entities;
using Book4H2Ten.EntityFrameWorkCore.Repositories;
using Book4H2Ten.Services.Books.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Books
{
    public class BookService : BaseService<Book> , IBookService
    {
        private readonly IRepository<Book_TypeBook> _book_TypeBookRepository;

        public BookService(IRepository<Book> repository, IHttpContextAccessor httpContextAccessor
            ,IRepository<Book_TypeBook> book_TypeBookRepository) : base(repository, httpContextAccessor)
        {
            _book_TypeBookRepository = book_TypeBookRepository;
        }

        public async Task<BookDtos> GetBookAsync(Guid bookId)
        {
            var book = await _repository.GetByIdAsync(bookId);
            return new BookDtos 
            { 
                BookName = book.BookName,
                Description = book.Description,
                PublishDate = book.PublishDate,
                Image = book.Image,
                Quantity = book.Quantity,
                Price = book.Price,
                AuthorName = book.AuthorName,
                Status = book.Status
            };
        }

        public async Task<BookDtos> CreateBook(BookDtos bookDtos, Guid typeBookId)
        {
            var newBook = new Book
            {
                BookName = bookDtos.BookName,
                Description= bookDtos.Description,
                PublishDate = bookDtos.PublishDate,
                Image = bookDtos.Image,
                Quantity = bookDtos.Quantity,
                Price= bookDtos.Price,
                AuthorName = bookDtos.AuthorName,
                Status = bookDtos.Status
            };
            await _repository.AddAsync(newBook);

            var addBook_TypeBook = new Book_TypeBook
            {
                BookId = newBook.GuidId,
                TypeBookId = typeBookId
            };
            
            await _book_TypeBookRepository.AddAsync(addBook_TypeBook);

            return bookDtos;
        }

        public async Task<BookDtos> EditBook(BookDtos bookDtos, Guid bookId)
        {
            var books = await _repository.GetByIdAsync(bookId);
            books.BookName = bookDtos.BookName;
            books.Description = bookDtos.Description;
            books.PublishDate = bookDtos.PublishDate; 
            books.Image = bookDtos.Image;
            books.Quantity = bookDtos.Quantity;
            books.Price = bookDtos.Price;
            books.AuthorName = bookDtos.AuthorName;
            books.Status = bookDtos.Status;

            await _repository.UpdateAsync(books);
            return bookDtos;
        }

        public async Task DeleteBook(Guid bookId)
        {
            var books = await _repository.GetByIdAsync(bookId);
            await _repository.DeleteAsync(books);
        }
    }
}
