using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.DTOs.AuthorDTOs
{
    public class DisplayAuthorDTO : IErrorMessage
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int BooksCount { get; set; }
        public string ErrorMessage { get; set; }
    }
}
