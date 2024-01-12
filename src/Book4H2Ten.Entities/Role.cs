using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }

    }
    public class RoleConfiguration : BaseEntityTypeConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);
        }
    }
}
