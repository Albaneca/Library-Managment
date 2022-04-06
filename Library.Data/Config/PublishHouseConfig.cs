using Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Config
{
    class PublishHouseConfig : IEntityTypeConfiguration<PublishHouse>
    {
        public void Configure(EntityTypeBuilder<PublishHouse> builder)
        {
            builder.HasMany(a => a.Books)
                .WithOne(p => p.PublishHouse)
                .HasForeignKey(p => p.PublishHouseId);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
