using Microsoft.AspNetCore.Http;

namespace Library.Web.Models
{
    public class PublishHouseViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public int BookCount { get; set; }
    }
}
