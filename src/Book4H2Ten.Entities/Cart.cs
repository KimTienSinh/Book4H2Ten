using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Entities
{
    public class Cart : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid BookId { get; set; }

        public double PriceTotalLine { get; set; }

        public int Quantity { get; set; }

        public int Status { get; set; } 
    }
    public class CartConfiguration : BaseEntityTypeConfiguration<Cart>
    {
        public override void Configure(EntityTypeBuilder<Cart> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(x => x.BookId);
            entityTypeBuilder.HasIndex(x => x.UserId);
            base.Configure(entityTypeBuilder);
        }
    }
}
