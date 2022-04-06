using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Services.DTOs
{
    public class CreateBanDTO
    {
        [Required]
        public string Reason { get; set; }
        public DateTime? Days { get; set; }
        public string Email { get; set; }

        public bool isSent { get; set; }
    }
}
