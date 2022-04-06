using Library.Services.DTOs.PublishHouseDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface IPublishHouseService
    {
        Task<IEnumerable<DisplayPublishHouseDTO>> GetAsync(int page);
        Task<DisplayPublishHouseDTO> PostAsync(CreatePublishHouseDTO obj);
        Task<DisplayPublishHouseDTO> UpdateAsync(long id, CreatePublishHouseDTO obj);
        Task<DisplayPublishHouseDTO> DeleteAsync(long id);
        Task<IEnumerable<DisplayPublishHouseDTO>> FilterPublishHousesAsync(int page, string part);
        Task<DisplayPublishHouseDTO> GetHouseByNameOrIdAsync(string nameOrId);
        Task<DisplayPublishHouseDTO> UpdatePicture(long id, string pictureURL);
        Task<int> HouseCountAsync();
    }
}
