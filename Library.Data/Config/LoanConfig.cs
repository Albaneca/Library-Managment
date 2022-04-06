using Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Config
{
    class LoanConfig : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(f => f.BookId);

            builder.HasOne(l => l.Requester)
                .WithMany(r => r.Loans)
                .HasForeignKey(f => f.RequesterId)
                .OnDelete(DeleteBehavior.NoAction);
                   

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
