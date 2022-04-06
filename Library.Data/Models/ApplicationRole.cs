using System.Collections.Generic;

namespace Library.Data.Models
{
    public class ApplicationRole : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}