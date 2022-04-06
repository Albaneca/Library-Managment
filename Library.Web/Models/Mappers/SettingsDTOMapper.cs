using Library.Services.DTOs;

namespace Library.Web.Models.Mappers
{
    public static class SettingsDTOMapper
    {
        public static SettingsViewModel GetDTO(this UserDTO user)
        {
            return new SettingsViewModel
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Role = user.Role,
                ProfilePictureLink = user.ImageLink,
                RegisteredAt = user.Created,
                Loans = user.Loans
            };
        }

        public static UserDTO GetDTO(this SettingsViewModel user)
        {
            return new UserDTO
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = user.NewPassword,
                Role = user.Role
            };
        }
    }
}