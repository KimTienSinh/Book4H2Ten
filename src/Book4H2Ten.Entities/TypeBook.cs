using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Entities
{
    public class TypeBook : BaseEntity
    {
        public string TypeBookName { get; set; }

    }
    public class TypeBookConfiguration : BaseEntityTypeConfiguration<TypeBook>
    {
        public override void Configure(EntityTypeBuilder<TypeBook> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);
        }
    }
}
