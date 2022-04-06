using Library.Common;
using Library.Data.Models;
using Library.Services.DTOs.BookDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services.Mapper
{
    public static class BookMapper
    {
        public static DisplayBookDTO GetDTO(this Book book)
        {
            if (book == null)
            {
                return new DisplayBookDTO() { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            return new DisplayBookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                Year = book.Year,
                AuthorsName = book.Author.Name,
                Description = book.Description,
                AuthorId = book.AuthorId,
                PublishHouseId = book.PublishHouseId,
                IsLoaned = book.IsLoaned,
                PublishHouseName = book.PublishHouse.Name,
                URL = book.URL,
                TotalLoans = book.Loans == null ? 0 : book.Loans.Count()
            };
        }

        public static Book GetEntity(this CreateBookDTO book)
        {
            return new Book()
            {
                AuthorId = book.AuthorId,
                Description = book.Description,
                PublishHouseId = book.PublishHouseId,
                Title = book.Title,
                Year = book.Year
            };
        }
    }
}
