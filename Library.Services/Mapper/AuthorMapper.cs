using Library.Common;
using Library.Data.Models;
using Library.Services.DTOs.AuthorDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services.Mapper
{
    public static class AuthorMapper
    {
        public static DisplayAuthorDTO GetDTO(this Author author)
        {
            if (author == null)
            {
                return new DisplayAuthorDTO() { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            return new DisplayAuthorDTO()
            {
                Id = author.Id,
                Name = author.Name,
                URL = author.URL,
                BooksCount = author.Books == null ? 0: author.Books.Count()
            };
        }

        public static Author GetEntity(this CreateAuthorDTO author)
        {
            return new Author()
            {
                Name = author.Name
            };
        }
    }
}
