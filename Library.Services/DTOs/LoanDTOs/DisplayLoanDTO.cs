using Library.Common;
using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.DTOs.LoanDTOs
{
    public class DisplayLoanDTO : IErrorMessage
    {
        public long Id { get; set; }
        public DateTime? DueDate { get; set; }
        public string BookName { get; set; }
        public string RequesterName { get; set; }
        public string ApproverName { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
    }
}
