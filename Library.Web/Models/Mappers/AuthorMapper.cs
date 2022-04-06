using Library.Services.DTOs.AuthorDTOs;

namespace Library.Web.Models.Mappers
{
    public static class AuthorMapper
    {
        public static CreateAuthorDTO GetDTO(this AuthorViewModel model)
        {
            var entity = new CreateAuthorDTO
            {
                Name = model.Name,
            };
            return entity;
        }

        public static AuthorViewModel GetModel(this DisplayAuthorDTO author)
        {
            var entity = new AuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                BooksCount = author.BooksCount,
                URL = author.URL
            };
            return entity;
        }
    }
}
