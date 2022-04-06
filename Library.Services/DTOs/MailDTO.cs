using Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Services.DTOs
{
    public class MailDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = GlobalConstants.INVALID_EMAIL)]
        public string Reciever { get; set; }

        public string EmailFrom { get; set; }

        public string Subject { get; set; }

        public string Phone { get; set; }

        [Required]
        public string Message { get; set; }

        public bool isSent { get; set; }

        public bool isFromContact { get; set; }

        public bool ResetPassword { get; set; }
        public bool IsBan { get; set; }
    }
}
