using Library.Services.DTOs.BookDTOs;

namespace Library.Web.Models.Mappers
{
    public static class BookMapper
    {
        public static CreateBookDTO GetDTO(this BookViewModel model)
        {
            var entity = new CreateBookDTO
            {
                Title = model.Title,
                Description = model.Description,
                Year = model.Year,
                AuthorId = model.AuthorId,
                PublishHouseId = model.PublishHouseId
            };
            return entity;
        }

        public static BookViewModel GetModel(this DisplayBookDTO book)
        {
            var entity = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Year = book.Year,
                URL = book.URL,
                AuthorId = book.AuthorId,
                PublishHouseId= book.PublishHouseId
            };
            return entity;
        }
    }
}
