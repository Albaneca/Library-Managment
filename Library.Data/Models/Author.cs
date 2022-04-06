using Library.Common;
using System.Collections.Generic;

namespace Library.Data.Models
{
    public class Author : DeletableEntity
    {
        public string Name { get; set; }
        public string URL { get; set; } = GlobalConstants.DefaultAuthorURL;
        public virtual ICollection<Book> Books { get; set; }
    }
}