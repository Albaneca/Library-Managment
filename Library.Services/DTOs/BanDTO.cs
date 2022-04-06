using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.DTOs
{
    public class BanDTO : IErrorMessage
    {
        public long Id { get; set; }

        public DateTime? BlockedOn { get; set; }

        public DateTime? BlockedDue { get; set; }

        public long UserId { get; set; }

        public string UserEmail { get; set; }

        public string Reason { get; set; }

        public string ErrorMessage { get; set; }

        public string Picture { get; set; }

        public string BanRemovedMessage { get; set; }
    }
}
