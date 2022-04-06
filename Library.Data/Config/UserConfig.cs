using Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Config
{
    class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(e => e.ApplicationRoleId);

            builder.HasOne(d => d.ApplicationRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ApplicationRoleId);

            builder.Property(e => e.Password).IsRequired();

            builder.HasCheckConstraint("Password_contains_space", "Password NOT LIKE '% %'");

            builder.Property(e => e.Email).IsRequired();

            builder.HasIndex(e => e.Email).IsUnique();

            builder.Property(e => e.FirstName).IsRequired();

            builder.Property(e => e.LastName).IsRequired();

            builder.Property(e => e.PhoneNumber).IsRequired();

            builder.HasIndex(e => e.PhoneNumber).IsUnique();

            builder.Property(e => e.Username).IsRequired();

            builder.HasIndex(e => e.Username).IsUnique();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }

}
