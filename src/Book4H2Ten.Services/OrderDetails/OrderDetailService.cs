using Book4H2Ten.Core.Errors;
using Book4H2Ten.Entities;
using Book4H2Ten.EntityFrameWorkCore.Repositories;
using Book4H2Ten.Services.OrderDetails.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.OrderDetails
{
    public class OrderDetailService : BaseService<OrderDetail>, IOrderDetailService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Book> _bookRepository;

        public OrderDetailService(IRepository<OrderDetail> repository, IHttpContextAccessor httpContextAccessor,
            IRepository<Order> orderRepository, IRepository<Book> bookRepository) : base(repository, httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
        }

        public async Task<OrderDetailDtos> GetOrderDetailAsync(Guid orderDetailId)
        {
            var orderDetail = await _repository.GetByIdAsync(orderDetailId);
            return new OrderDetailDtos
            {
                OrderId = orderDetail.OrderId,
                BookId = orderDetail.BookId,
                BookName = orderDetail.BookName,
                Quantity = orderDetail.Quantity,
                PriceTotalLine = orderDetail.PriceTotalLine,
                UnitBook = orderDetail.UnitBook
            };
        }

        public async Task<OrderDetailDtos> CreateOrderDetailAsync(OrderDetailDtos orderDetailDtos, Guid orderId, Guid bookId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            var book = await _bookRepository.GetByIdAsync(bookId);

            var newOrderDetail = new OrderDetail
            {
                OrderId = order.GuidId,
                BookId = book.GuidId,
                BookName = book.BookName,
                Quantity = book.Quantity,
                PriceTotalLine = orderDetailDtos.PriceTotalLine,
                UnitBook = orderDetailDtos.UnitBook
            };
            await _repository.AddAsync(newOrderDetail);
            return orderDetailDtos;
        }

        public async Task<OrderDetailDtos> EditOrderDetailAsync(Guid orderDetailId, OrderDetailDtos orderDetailDtos)
        {
            var orderDetail = await _repository.GetByIdAsync(orderDetailId);

            if(orderDetailDtos.Quantity != 0)
                orderDetail.Quantity = orderDetailDtos.Quantity;
            if(orderDetailDtos.PriceTotalLine != 0)
                orderDetail.PriceTotalLine = orderDetailDtos.PriceTotalLine;
            if(orderDetailDtos.UnitBook != "string")
                orderDetail.UnitBook = orderDetailDtos.UnitBook;

            await _repository.UpdateAsync(orderDetail);
            return orderDetailDtos;
        }

        public async Task DeleteOrderDetailAsync(Guid orderDetailId)
        {
            var orderDetail = await _repository.GetByIdAsync(orderDetailId);
            await _repository.DeleteAsync(orderDetail);
        }
    }

}
