using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.DTOs
{
    public class ResponseAuthDTO
    {
        public string Email { get; set; }

        public bool? isBlocked { get; set; }

        public string BlockedDue { get; set; }

        public string Role { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }

    }
}
