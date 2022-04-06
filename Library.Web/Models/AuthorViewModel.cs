using Microsoft.AspNetCore.Http;

namespace Library.Web.Models
{
    public class AuthorViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int BooksCount { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public string URL { get; set; }
    }
}
