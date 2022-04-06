using Library.Data.Models;
using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.DTOs
{
    public class DisplayUserDTO : IErrorMessage
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageLink { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsBlocked { get; set; }
        public int LoansCount { get;set; }

    }
}
