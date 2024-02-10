using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Services.Carts.Dtos
{
    public class CartDtos
    {
        public Guid UserId { get; set; }

        public Guid BookId { get; set; }

        public double PriceTotalLine { get; set; }

        public int Quantity { get; set; }

        public int Status { get; set; }
    }
}
