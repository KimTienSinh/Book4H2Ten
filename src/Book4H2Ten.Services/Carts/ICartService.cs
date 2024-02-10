using Book4H2Ten.Services.Carts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Carts
{
    public interface ICartService
    {
        Task<CartDtos> GetCartAsync(Guid cartId);
        Task<CartDtos> CreateCartAsync(CartDtos cartDtos, Guid bookId, Guid userId);
        Task<CartDtos> EditCartAsync(CartDtos cartDtos, Guid cartId);
        Task DeleteCartAsync(Guid cartId);

    }
}
