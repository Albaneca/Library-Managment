using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.DTOs.BookDTOs
{
    public class DisplayBookDTO : IErrorMessage
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Year { get; set; }
        public string AuthorsName { get; set; }
        public long AuthorId { get; set; }
        public string PublishHouseName { get; set; }
        public long PublishHouseId { get; set; }
        public bool IsLoaned { get; set; }
        public int TotalLoans { get; set; }
        public string URL { get; set; }
        public string ErrorMessage { get; set; }
    }
}
