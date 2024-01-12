using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Entities
{
    public class UserRole : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
    public class UserRoleConfiguration : BaseEntityTypeConfiguration<UserRole>
    {
        public override void Configure(EntityTypeBuilder<UserRole> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(x => x.RoleId);
            entityTypeBuilder.HasIndex(x => x.UserId);
            base.Configure(entityTypeBuilder);
        }
    }
}
