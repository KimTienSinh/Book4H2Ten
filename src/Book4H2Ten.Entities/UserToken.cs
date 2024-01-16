using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Entities
{
    public class UserToken : BaseEntity
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiredTime { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiredTime { get; set; }
    }

    public class UserTokenConfiguration : BaseEntityTypeConfiguration<UserToken>
    {
        public override void Configure(EntityTypeBuilder<UserToken> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(x => x.UserId);
            base.Configure(entityTypeBuilder);
        }
    }
}
