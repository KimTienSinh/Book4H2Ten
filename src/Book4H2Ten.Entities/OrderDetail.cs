using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Entities
{
    public class OrderDetail : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public int Quantity { get; set; }
        public double PriceTotalLine { get; set; }
        public string UnitBook { get; set; }

    }
    public class OrderDetailConfiguration : BaseEntityTypeConfiguration<OrderDetail>
    {
        public override void Configure(EntityTypeBuilder<OrderDetail> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(x => x.BookId);
            entityTypeBuilder.HasIndex(x => x.OrderId);
            base.Configure(entityTypeBuilder);
        }
    }
}
