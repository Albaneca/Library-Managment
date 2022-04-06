using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.DTOs
{
    public class InboxDTO
    {
        public string Sender { get; set; }

        public string Receiver { get; set; }

        public DateTime SendOnDate { get; set; }

        public string Message { get; set; }
    }
}
