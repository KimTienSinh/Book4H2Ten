using Book4H2Ten.Services.TypeBooks.Dtos;
using Book4H2Ten.Services.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.TypeBooks
{
    public interface ITypeBookService
    {
        Task<TypeBookDtos> GetTypeBookAsync(Guid typeBookId);
        Task<TypeBookDtos> CreateTypeBook(TypeBookDtos typeBookDtos);
        Task<TypeBookDtos> EditTypeBook(Guid typeBookId, TypeBookDtos typeBookDtos);
        Task DeleteTypeBook(Guid typeBookId);
    }
}
