using Library.Common;
using Library.Services.DTOs;

namespace Library.Web.Models.Mappers
{
    public static class GetUserDTO
    {
        public static RegisterUserDTO GetDTO(this RegisterViewModel user)
        {
            return new RegisterUserDTO
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password
            };
        }
    }
}
