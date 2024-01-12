using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Entities
{
    public class Book_TypeBook : BaseEntity
    {
        public Guid BookId { get; set; }
       
        public Guid TypeBookId { get; set; }

    }
    public class Book_TypeBookConfiguration : BaseEntityTypeConfiguration<Book_TypeBook>
    {
        public override void Configure(EntityTypeBuilder<Book_TypeBook> entityTypeBuilder)
        {
            //entityTypeBuilder.Property(x => x.BookName).IsRequired();
            entityTypeBuilder.HasIndex(x => x.BookId);
            entityTypeBuilder.HasIndex(x => x.TypeBookId);
            base.Configure(entityTypeBuilder);
        }
    }
}
