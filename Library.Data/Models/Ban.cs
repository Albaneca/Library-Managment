using System;

namespace Library.Data.Models
{
    public class Ban : DeletableEntity
    {
        public DateTime? BlockedOn { get; set; }

        public DateTime? BlockedDue { get; set; }

        public long UserId { get; set; }

        public string Reason { get; set; }

        public virtual User User { get; set; }
    }
}