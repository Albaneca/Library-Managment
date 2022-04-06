using Library.Common;
using System;

namespace Library.Data.Models
{
    public class Loan : DeletableEntity
    {
        public DateTime? DueDate { get; set; }
        public long BookId { get; set; }
        public long RequesterId { get; set; }
        public long? ApproverId { get; set; }
        public string Status { get; set; } = GlobalConstants.LOAN_NOT_CONFIRMED;
        public Book Book { get; set; }
        public User Requester { get; set; }
        public User Approver { get; set; }

    }
}