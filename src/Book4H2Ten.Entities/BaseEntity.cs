using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book4H2Ten.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            GuidId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long Id { get; set; }

        public virtual Guid GuidId { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual DateTime? UpdatedAt { get; set; }

        public virtual DateTime? DeletionTime { get; set; }

        public virtual bool IsDeleted { get; set; }

    }

    public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TBase> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(x => x.GuidId).IsUnique(true);
        }
    }
}
