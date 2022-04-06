using Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Config
{
    class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasOne(b => b.PublishHouse).WithMany(b => b.Books);
            builder.HasOne(b => b.Author).WithMany(b => b.Books);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
