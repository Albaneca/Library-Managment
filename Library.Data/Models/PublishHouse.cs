using Library.Common;
using System.Collections.Generic;

namespace Library.Data.Models
{
    public class PublishHouse : DeletableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; } = GlobalConstants.DefaultPublishHouseURL;
        public virtual ICollection<Book> Books { get; set; }
    }
}