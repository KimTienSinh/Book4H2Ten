using Book4H2Ten.Services.Carts.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Book4H2Ten.Host.BaseController;
using Book4H2Ten.Services.Carts;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;


namespace Book4H2Ten.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : WebBaseController
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [SwaggerOperation(Summary = "Get cart")]
        [AllowAnonymous]
        [HttpGet("{cartId}")]
        public async Task<CartDtos> GetCartAsync(Guid cartId)
            => await _cartService.GetCartAsync(cartId);

        [SwaggerOperation(Summary = "Create cart")]
        [AllowAnonymous]
        [HttpPost("{bookId}/{userId}")]
        public async Task<CartDtos> CreateCartAsync(CartDtos cartDtos, Guid bookId, Guid userId)
            => await _cartService.CreateCartAsync(cartDtos, bookId, userId);

        [SwaggerOperation(Summary = "Create cart")]
        [AllowAnonymous]
        [HttpPut("{cartId}")]
        public async Task<CartDtos> EditCartAsync(CartDtos cartDtos, Guid cartId)
            => await _cartService.EditCartAsync(cartDtos, cartId);

        [SwaggerOperation(Summary = "Create cart")]
        [AllowAnonymous]
        [HttpDelete("{cartId}")]
        public async Task DeleteCartAsync(Guid cartId)
            => await _cartService.DeleteCartAsync(cartId);
    }
}
