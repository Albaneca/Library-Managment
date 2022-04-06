using Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Config
{
    class InboxConfig : IEntityTypeConfiguration<Inbox>
    {
        public void Configure(EntityTypeBuilder<Inbox> builder)
        {
            builder.HasIndex(e => e.UserId);

            builder.HasOne(d => d.User)
                .WithMany(p => p.InboxMessages)
                .HasForeignKey(d => d.UserId);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }

}
