using Library.Services.DTOs.BookDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Library.Web.Models
{
    public class BookViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Year { get; set; }
        public long AuthorId { get; set; }
        public long PublishHouseId { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public string URL { get; set; }
        public SelectList Authors { get; set; }
        public SelectList PublishHouses { get; set; }

    }
}
