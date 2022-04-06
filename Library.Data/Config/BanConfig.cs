using Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Config
{
    public class BanConfig : IEntityTypeConfiguration<Ban>
    {
        public void Configure(EntityTypeBuilder<Ban> builder)
        {
            builder.HasIndex(e => e.UserId)
                    .IsUnique();

            builder.HasOne(d => d.User)
                .WithOne(p => p.Ban)
                .HasForeignKey<Ban>(d => d.UserId);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }

}
