using Book4H2Ten.Services.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Orders
{
    public interface IOrderService
    {
        Task<OrderDtos> GetOrderAsync(Guid orderId);
        Task<OrderDtos> CreateOrderAsync(OrderDtos orderDtos, Guid UserId);
        Task<OrderDtos> EditOrderAsync(OrderDtos orderDtos, Guid orderId);
        Task DeleteOrderAsync(Guid orderId);
    }
}
