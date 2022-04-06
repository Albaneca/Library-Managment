namespace Library.Data.Models
{
    public class Inbox : DeletableEntity
    {
        public long FromUserId { get; set; }

        public long UserId { get; set; }

        public string Message { get; set; }

        public bool Seen { get; set; }

        public virtual User User { get; set; }

    }
}