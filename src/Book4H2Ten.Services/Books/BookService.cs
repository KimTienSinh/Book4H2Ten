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
        public BookService(IRepository<Book> repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {
            
        }

        /*public Task<BookDtos> CreateBook(BookDtos book)
        {

        }*/
    }
}
