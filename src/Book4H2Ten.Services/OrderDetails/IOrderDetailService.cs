using Book4H2Ten.Services.OrderDetails.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.OrderDetails
{
    public interface IOrderDetailService
    {
        Task<OrderDetailDtos> GetOrderDetailAsync(Guid orderDetailId);
        Task<OrderDetailDtos> CreateOrderDetailAsync(OrderDetailDtos orderDetailDtos, Guid orderId, Guid bookId);
        Task<OrderDetailDtos> EditOrderDetailAsync(Guid orderDetailId, OrderDetailDtos orderDetailDtos);
        Task DeleteOrderDetailAsync(Guid orderDetailId);
    }
}
