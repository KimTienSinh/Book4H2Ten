﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Book4H2Ten.Core.Enums.EnumLibrary;

namespace Book4H2Ten.Entities
{
    public class User : BaseEntity
    {
        //class

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName {  get; set; }
        public string LastName { get; set; }

        public UserGender Gender { get; set; }

        public RoleName RoleName { get; set; }

        public DateTime BirtDate { get; set; }

        public string Address { get; set; }
        public int Status { get; set; }

        public string NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public string VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
    }
    public class UserConfiguration : BaseEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.UserName).IsRequired();
            entityTypeBuilder.Property(x => x.Email).IsRequired();
            entityTypeBuilder.Property(x => x.Password).IsRequired();
            base.Configure(entityTypeBuilder);
        }
    }
}
