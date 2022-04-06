using Library.Common;
using Library.Data;
using Library.Services.Contracts;
using Library.Services.DTOs.PublishHouseDTOs;
using Library.Services.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public class PublishHouseService : IPublishHouseService
    {
        private readonly LibraryDbContext _db;
        public PublishHouseService(LibraryDbContext db)
        {
            _db = db;
        }
        public async Task<DisplayPublishHouseDTO> DeleteAsync(long id)
        {
            var house = await _db.PublishHouses
                .Include(x => x.Books)
                .ThenInclude(b => b.Loans)
                              .FirstOrDefaultAsync(x => x.Id == id);

            if (house is null)
            {
                return new DisplayPublishHouseDTO { ErrorMessage = GlobalConstants.HOUSE_NOT_FOUND };
            }

            var houseDTO = house.GetDTO();
            foreach (var book in house.Books)
            {
                _db.Books.Remove(book);
                foreach (var loan in book.Loans)
                {
                    _db.Loans.Remove(loan);
                }
            }
            _db.PublishHouses.Remove(house);
            await _db.SaveChangesAsync();

            return houseDTO;
        }

        public async Task<IEnumerable<DisplayPublishHouseDTO>> GetAsync(int page)
        {
            return await _db.PublishHouses
                 .Include(p => p.Books)
                 .Skip(page * GlobalConstants.PageSkip)
                 .Take(10)
                 .Select(x => x.GetDTO())
                 .ToListAsync();
        }

        public async Task<IEnumerable<DisplayPublishHouseDTO>> FilterPublishHousesAsync(int page, string part)
        {
            return await _db.PublishHouses.Where(x => x.Name.Contains(part))
                                           .Include(p => p.Books)
                                           .Skip(page * GlobalConstants.PageSkip)
                                           .Take(10)
                                           .Select(x => x.GetDTO())
                                           .ToListAsync();
        }

        public async Task<DisplayPublishHouseDTO> GetHouseByNameOrIdAsync(string nameOrId)
        {
            var house = await _db.PublishHouses
                                     .Include(p => p.Books)
                                     .Where(x => x.Name.Contains(nameOrId) || x.Id.ToString() == nameOrId)
                                     .Select(x => x.GetDTO())
                                     .FirstOrDefaultAsync();

            if (house is null)
            {
                return new DisplayPublishHouseDTO { ErrorMessage = GlobalConstants.HOUSE_NOT_FOUND };
            }

            return house;
        }

        public async Task<DisplayPublishHouseDTO> PostAsync(CreatePublishHouseDTO obj)
        {
            if (obj.Name == null)
            {
                return new DisplayPublishHouseDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }
            if (await _db.PublishHouses.FirstOrDefaultAsync(x => x.Name == obj.Name) != null)
            {
                return new DisplayPublishHouseDTO { ErrorMessage = GlobalConstants.HOUSE_EXIST };
            }

            var newHouse = obj.GetEntity();

            await _db.PublishHouses.AddAsync(newHouse);
            await _db.SaveChangesAsync();

            var lastHouse = _db.PublishHouses.Count();

            return await _db.PublishHouses.Where(x => x.Id == lastHouse)
                                  .Select(x => x.GetDTO())
                                  .FirstOrDefaultAsync();
        }

        public async Task<DisplayPublishHouseDTO> UpdateAsync(long id, CreatePublishHouseDTO obj)
        {
            if (await _db.PublishHouses.FirstOrDefaultAsync(x => x.Name == obj.Name) != null)
            {
                return new DisplayPublishHouseDTO { ErrorMessage = GlobalConstants.HOUSE_EXIST };
            }

            var house = await _db.PublishHouses.FirstOrDefaultAsync(x => x.Id == id);

            if (house is null)
            {
                return new DisplayPublishHouseDTO { ErrorMessage = GlobalConstants.HOUSE_NOT_FOUND };
            }

            house.Name = obj.Name;
            house.Description = obj.Description;

            await _db.SaveChangesAsync();

            return house.GetDTO();
        }

        public async Task<DisplayPublishHouseDTO> UpdatePicture(long id, string pictureURL)
        {
            var house = await _db.PublishHouses
                                    .Include(a => a.Books)
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (house is null)
            {
                return new DisplayPublishHouseDTO { ErrorMessage = GlobalConstants.BOOK_NOT_FOUND };
            }
            house.URL = pictureURL;
            await _db.SaveChangesAsync();

            return house.GetDTO();
        }

        public async Task<int> HouseCountAsync()
        {
            return await _db.PublishHouses.CountAsync();
        }
    }
}
