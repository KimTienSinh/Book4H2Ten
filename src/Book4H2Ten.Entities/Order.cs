using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShippingAddress { get; set; }
        public double PriceTotal { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
    }
    public class OrderConfiguration : BaseEntityTypeConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(x => x.UserId);
            base.Configure(entityTypeBuilder);
        }
    }
}
