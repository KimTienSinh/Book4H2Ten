using Book4H2Ten.Host.BaseController;
using Book4H2Ten.Services.OrderDetails;
using Book4H2Ten.Services.OrderDetails.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using System;

namespace Book4H2Ten.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : WebBaseController
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController (IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [SwaggerOperation(Summary = "Get order detail")]
        [AllowAnonymous]
        [HttpGet("{orderDetailId}")]
        public async Task<OrderDetailDtos> GetOrderDetailAsync(Guid orderDetailId)
            => await _orderDetailService.GetOrderDetailAsync(orderDetailId);

        [SwaggerOperation(Summary = "Create order detail")]
        [AllowAnonymous]
        [HttpPost("{orderId}/{bookId}")]
        public async Task<OrderDetailDtos> CreateOrderDetailAsync(OrderDetailDtos orderDetailDtos, Guid orderId, Guid bookId)
            => await _orderDetailService.CreateOrderDetailAsync(orderDetailDtos, orderId, bookId);

        [SwaggerOperation(Summary = "Edit order detail")]
        [AllowAnonymous]
        [HttpPatch("{orderDetailId}")]
        public async Task<OrderDetailDtos> EditOrderDetailAsync(Guid orderDetailId, OrderDetailDtos orderDetailDtos)
            => await _orderDetailService.EditOrderDetailAsync(orderDetailId, orderDetailDtos);

        [SwaggerOperation(Summary = "Delete order detail")]
        [AllowAnonymous]
        [HttpDelete("{orderDetailId}")]
        public async Task DeleteOrderDetailAsync(Guid orderDetailId)
            => await _orderDetailService.DeleteOrderDetailAsync(orderDetailId);
    }
}
