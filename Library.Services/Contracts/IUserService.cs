using Library.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> FilterUsersAsync(int page, string part);

        Task<IEnumerable<DisplayUserDTO>> GetAsync(int page);

        Task<UserDTO> GetUserByEmailOrIdAsync(string emailOrId);

        Task<UserDTO> PostAsync(RegisterUserDTO obj);

        Task<UserDTO> UpdateAsync(string email, UserDTO obj);

        Task<UserDTO> UpdateProfilePicture(string email, string profilePictureURL);

        Task<UserDTO> DeleteAsync(string email);

        Task<UserDTO> UpdatePasswordAsync(string email, string newPassword);

        Task<int> UsersCountAsync();

    }
}
