using Book4H2Ten.Host.BaseController;
using Book4H2Ten.Services.Orders;
using Book4H2Ten.Services.Orders.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Book4H2Ten.Entities;

namespace Book4H2Ten.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : WebBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [SwaggerOperation(Summary = "Get Order")]
        [AllowAnonymous]
        [HttpGet("{orderId}")]
        public async Task<OrderDtos> GetOrderAsync(Guid orderId)
            => await _orderService.GetOrderAsync(orderId);

        [SwaggerOperation(Summary = "Create Order")]
        [AllowAnonymous]
        [HttpPost("{UserId}")]
        public async Task<OrderDtos> CreateOrderAsync(OrderDtos orderDtos, Guid UserId)
            => await _orderService.CreateOrderAsync(orderDtos, UserId);

        [SwaggerOperation(Summary = "Edit Order")]
        [AllowAnonymous]
        [HttpPatch("{orderId}")]
        public async Task<OrderDtos> EditOrderAsync(OrderDtos orderDtos, Guid orderId)
            => await _orderService.EditOrderAsync(orderDtos, orderId);

        [SwaggerOperation(Summary = "Delete Order")]
        [AllowAnonymous]
        [HttpDelete("{orderId}")]
        public async Task DeleteOrderAsync(Guid orderId) 
            => await _orderService.DeleteOrderAsync(orderId);
    }
}
