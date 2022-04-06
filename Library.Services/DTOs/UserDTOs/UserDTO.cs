using Library.Data.Models;
using Library.Services.Contracts;
using Library.Services.DTOs.LoanDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.DTOs
{
    public class UserDTO : IErrorMessage
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsBlocked { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
        public string ImageLink { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Created { get; set; }
        public List<Loan> Loans { get; set; }
    }
}
