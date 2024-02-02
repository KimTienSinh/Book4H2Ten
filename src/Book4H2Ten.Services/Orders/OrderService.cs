using Book4H2Ten.Entities;
using Book4H2Ten.EntityFrameWorkCore.Repositories;
using Book4H2Ten.Services.Orders.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Orders
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        public OrderService(IRepository<Order> repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {
        }

        public async Task<OrderDtos> GetOrderAsync (Guid orderId)
        {
            var order = await _repository.GetByIdAsync(orderId);
            return new OrderDtos
            {
                UserId = order.UserId,
                FirstName = order.FirstName,
                LastName = order.LastName,
                ShippingAddress = order.ShippingAddress,
                PriceTotal = order.PriceTotal,
                Note = order.Note,
                Status = order.Status
            };
        }

        public async Task<OrderDtos> CreateOrderAsync (OrderDtos orderDtos, Guid UserId)
        {
            var newOrder = new Order
            {
                UserId = UserId,
                FirstName = orderDtos.FirstName,
                LastName = orderDtos.LastName,
                ShippingAddress = orderDtos.ShippingAddress,
                PriceTotal = orderDtos.PriceTotal,
                Note = orderDtos.Note,
                Status = orderDtos.Status
            };
            await _repository.AddAsync(newOrder);

            return orderDtos;
        }

        public async Task<OrderDtos> EditOrderAsync (OrderDtos orderDtos, Guid orderId)
        {
            var order = await _repository.GetByIdAsync(orderId);

            if (orderDtos.FirstName != "string")
                order.FirstName = orderDtos.FirstName;

            if (orderDtos.LastName != "string")
                order.LastName = orderDtos.LastName;

            if (orderDtos.ShippingAddress != "string")
                order.ShippingAddress = orderDtos.ShippingAddress;

            if (orderDtos.PriceTotal != 0)
                order.PriceTotal = orderDtos.PriceTotal;

            if (orderDtos.Note != "string")
                order.Note = orderDtos.Note;

            await _repository.UpdateAsync(order);
            return orderDtos;
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            var order = await _repository.GetByIdAsync(orderId);
            await _repository.DeleteAsync(order);
        }
    }
}
