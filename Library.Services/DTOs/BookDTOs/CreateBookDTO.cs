using System;

namespace Library.Services.DTOs.BookDTOs
{
    public class CreateBookDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Year { get; set; }
        public long AuthorId { get; set; }
        public long PublishHouseId { get; set; }
    }
}
