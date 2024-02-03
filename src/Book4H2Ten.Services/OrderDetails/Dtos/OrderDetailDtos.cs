using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.OrderDetails.Dtos
{
    public class OrderDetailDtos
    {
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public int Quantity { get; set; }
        public double PriceTotalLine { get; set; }
        public string UnitBook { get; set; }
    }
}
