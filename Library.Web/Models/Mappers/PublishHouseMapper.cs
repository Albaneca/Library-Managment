using Library.Services.DTOs.PublishHouseDTOs;

namespace Library.Web.Models.Mappers
{
    public static class PublishHouseMapper
    {
        public static CreatePublishHouseDTO GetDTO(this PublishHouseViewModel model)
        {
            var entity = new CreatePublishHouseDTO
            {
                Name = model.Name,
                Description = model.Description,
            };
            return entity;
        }

        public static PublishHouseViewModel GetModel(this DisplayPublishHouseDTO house)
        {
            var entity = new PublishHouseViewModel
            {
                Id = house.Id,
                Name = house.Name,
                Description=house.Description,
                BookCount = house.BookCount,
                URL = house.URL,
            };
            return entity;
        }
    }
}
