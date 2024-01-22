using Book4H2Ten.Host.BaseController;
using Book4H2Ten.Services.TypeBooks;
using Book4H2Ten.Services.TypeBooks.Dtos;
using Book4H2Ten.Services.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Book4H2Ten.Host.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TypeBookController : WebBaseController
    {
        private readonly ITypeBookService _typeBookService;

        public TypeBookController(ITypeBookService typeBookService)
        {
            _typeBookService = typeBookService;
        }

        [SwaggerOperation(Summary = "Get type book detail")]
        [AllowAnonymous]
        [HttpGet("{typeBookId}")]
        public async Task<TypeBookDtos> GetTypeBookAsync(Guid typeBookId)
            => await _typeBookService.GetTypeBookAsync(typeBookId);

        [SwaggerOperation(Summary = "Create type book")]
        [AllowAnonymous]
        [HttpPost("CreateTypeBook")]
        public async Task<TypeBookDtos> CreateTypeBook(TypeBookDtos typeBookDtos)
            => await _typeBookService.CreateTypeBook(typeBookDtos);


        [SwaggerOperation(Summary = "Edit type book")]
        [AllowAnonymous]
        [HttpPatch("{typeBookId}")]
        public async Task<TypeBookDtos> EditTypeBook(Guid typeBookId, TypeBookDtos typeBookDtos)
            => await _typeBookService.EditTypeBook(typeBookId, typeBookDtos);

        [SwaggerOperation(Summary = "Delete type book")]
        [AllowAnonymous]
        [HttpDelete("{typeBookId}")]
        public async Task DeleteTypeBook(Guid typeBookId)
            => await _typeBookService.DeleteTypeBook(typeBookId);

    }
}
