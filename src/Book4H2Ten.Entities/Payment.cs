using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Entities
{
    public class Payment : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid PaymentId { get; set; }
        public string PaymentName { get; set; }
    }

    public class PaymentConfiguration : BaseEntityTypeConfiguration<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(x => x.PaymentId);
            entityTypeBuilder.HasIndex(x => x.UserId);
            base.Configure(entityTypeBuilder);
        }
    }
}
