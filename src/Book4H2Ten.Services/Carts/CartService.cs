using Book4H2Ten.Entities;
using Book4H2Ten.EntityFrameWorkCore.Repositories;
using Book4H2Ten.Services.Carts.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Carts
{
    public class CartService : BaseService<Cart>, ICartService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<User> _userRepository;

        public CartService(IRepository<Cart> repository, IHttpContextAccessor httpContextAccessor, 
            IRepository<Book> bookRepository, IRepository<User> userRepository) : base(repository, httpContextAccessor)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        public async Task<CartDtos> GetCartAsync(Guid cartId)
        {
            var cart = await _repository.GetByIdAsync(cartId);
            return new CartDtos
            {
                UserId = cart.UserId,
                BookId = cart.BookId,
                PriceTotalLine = cart.PriceTotalLine,
                Quantity = cart.Quantity
            };
        }

        public async Task<CartDtos> CreateCartAsync(CartDtos cartDtos, Guid bookId, Guid userId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            var user = await _userRepository.GetByIdAsync(userId);

            var cart = new Cart
            {
                UserId = user.GuidId,
                BookId = book.GuidId,
                PriceTotalLine = cartDtos.PriceTotalLine,
                Quantity = book.Quantity
            };

            await _repository.AddAsync(cart);

            return cartDtos;
        }

        public async Task<CartDtos> EditCartAsync(CartDtos cartDtos, Guid cartId)
        {
            var cart = await _repository.GetByIdAsync(cartId);
            //var findBook = await _bookRepository.GetByIdAsync(cart.BookId);

           /* if (cartDtos.BookId != Guid.Empty)
                cart.BookId = cartDtos.BookId;*/

            if (cartDtos.Quantity != 0)
                cart.Quantity = cartDtos.Quantity;
            if (cartDtos.PriceTotalLine != 0)
                cart.PriceTotalLine = cartDtos.PriceTotalLine;

            await _repository.UpdateAsync(cart);
            return cartDtos;
        }

        public async Task DeleteCartAsync(Guid cartId)
        {
            var cart = await _repository.GetByIdAsync(cartId);
            await _repository.DeleteAsync(cart);
        }
    }
}
