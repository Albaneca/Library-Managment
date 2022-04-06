using Library.Common;
using Library.Data.Models;
using Library.Services.DTOs.PublishHouseDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Mapper
{
    public static class PublishHouseMapper
    {
        public static DisplayPublishHouseDTO GetDTO(this PublishHouse house)
        {
            if (house == null)
            {
                return new DisplayPublishHouseDTO() { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            return new DisplayPublishHouseDTO()
            {
                Id = house.Id,
                Description = house.Description,
                Name = house.Name,
                URL = house.URL,
                BookCount = house.Books == null ? 0 : house.Books.Count
            };
        }

        public static PublishHouse GetEntity(this CreatePublishHouseDTO book)
        {
            return new PublishHouse()
            {
                Name = book.Name,
                Description = book.Description
            };
        }
    }
}
