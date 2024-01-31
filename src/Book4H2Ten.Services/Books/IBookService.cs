using Book4H2Ten.Services.Books.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Books
{
    public interface IBookService
    {
        Task<BookDtos> GetBookAsync(Guid bookId);
        Task<BookDtos> CreateBook(BookDtos bookDtos, Guid typeBookId);
        Task<BookDtos> EditBook(BookDtos bookDtos, Guid bookId);
        Task DeleteBook(Guid bookId);


    }
}
