using Book4H2Ten.Entities;
using Book4H2Ten.EntityFrameWorkCore.Repositories;
using Book4H2Ten.Services.TypeBooks.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.TypeBooks
{
    public class TypeBookService : BaseService<TypeBook>, ITypeBookService
    {
        public TypeBookService(IRepository<TypeBook> repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {

        }

        public async Task<TypeBookDtos> GetTypeBookAsync(Guid typeBookId)
        {
            var typeBook = await _repository.GetByIdAsync(typeBookId);
            return new TypeBookDtos { TypeBookName = typeBook.TypeBookName };
        }

        public async Task<TypeBookDtos> CreateTypeBook (TypeBookDtos typeBookDtos)
        {
            var typeBook = new TypeBook
            {
                TypeBookName = typeBookDtos.TypeBookName
            };
            await _repository.AddAsync(typeBook);
            //return await Task.FromResult<TypeBookDtos>(null);

            return typeBookDtos;
        }

        public async Task<TypeBookDtos> EditTypeBook (Guid typeBookId,TypeBookDtos typeBookDtos)
        {
            var typeBook = await _repository.GetByIdAsync(typeBookId);
            typeBook.TypeBookName = typeBookDtos.TypeBookName;
            await _repository.UpdateAsync(typeBook);
            return typeBookDtos;
        }

        public async Task DeleteTypeBook(Guid typeBookId)
        {
            var typeBook = await _repository.GetByIdAsync(typeBookId);
            await _repository.DeleteAsync(typeBook);
        }
    }
}
