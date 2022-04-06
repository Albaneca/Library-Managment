using Library.Common;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Library.Data.Models
{
    public class Book : DeletableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Year { get; set; }
        public long AuthorId { get; set; }
        public long PublishHouseId { get; set; }
        public string URL { get; set; } = GlobalConstants.DefaultBookURL;
        public Author Author { get; set; }
        public PublishHouse PublishHouse { get; set; }
        public bool IsLoaned { get; set; } = false;
        public virtual ICollection<Loan> Loans { get; set; }
    }
}