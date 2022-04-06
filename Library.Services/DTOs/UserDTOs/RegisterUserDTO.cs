using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.DTOs
{
    public class RegisterUserDTO
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}
