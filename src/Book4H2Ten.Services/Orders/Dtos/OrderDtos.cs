using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Orders.Dtos
{
    public class OrderDtos
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShippingAddress { get; set; }
        public double PriceTotal { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }
}
