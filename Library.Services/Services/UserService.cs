using Library.Common;
using Library.Data;
using Library.Data.Models;
using Library.Services.Contracts;
using Library.Services.DTOs;
using Library.Services.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryDbContext _db;
        public UserService(LibraryDbContext db)
        {
            _db = db;
        }
        public async Task<UserDTO> DeleteAsync(string email)
        {
            var user = await _db.Users
                    .Include(x => x.Ban)
                    .Include(x => x.ApplicationRole)
                    .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
            {
                return new UserDTO { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            }

            var userDTO = user.GetDTO();

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return userDTO;

        }

        public async Task<IEnumerable<UserDTO>> FilterUsersAsync(int page, string part)
        {
            return await _db.Users.Where(x => x.Email.Contains(part)
                                       || x.PhoneNumber.Contains(part)
                                       || x.Username.Contains(part))
                                           .Include(x => x.Loans)
                                               .ThenInclude(x => x.Book)
                                               .ThenInclude(x => x.Author)
                                           .Include(x => x.Loans)
                                               .ThenInclude(x => x.Book)
                                               .ThenInclude(x => x.PublishHouse)
                                           .Include(x => x.Ban)
                                           .Include(x => x.ApplicationRole)
                                           .Skip(page * GlobalConstants.PageSkip)
                                           .Take(10)
                                           .Select(x => x.GetDTO())
                                           .ToListAsync();
        }

        public async Task<IEnumerable<DisplayUserDTO>> GetAsync(int page)
        {
            return await _db.Users.Include(x => x.Loans)
                                     .ThenInclude(x => x.Book)
                                     .ThenInclude(x => x.Author)
                                 .Include(x => x.Loans)
                                     .ThenInclude(x => x.Book)
                                     .ThenInclude(x => x.PublishHouse)
                                 .Include(x => x.Ban)
                                 .Skip(page * GlobalConstants.PageSkip)
                                 .Take(10)
                                 .Select(x => x.GetDisplayDTO())
                                 .ToListAsync();
        }

        public async Task<UserDTO> GetUserByEmailOrIdAsync(string emailOrId)
        {
            var user = await _db.Users.Include(x => x.Loans)
                                         .ThenInclude(x => x.Book)
                                         .ThenInclude(x => x.Author)
                                     .Include(x => x.Loans)
                                         .ThenInclude(x => x.Book)
                                         .ThenInclude(x => x.PublishHouse)
                                     .Include(x => x.Ban)
                                     .Include(x => x.ApplicationRole)
                                     .Where(x => x.Email == emailOrId || x.Id.ToString() == emailOrId)
                                     .Select(x => x.GetDTO())
                                     .FirstOrDefaultAsync();

            if (user is null)
            {
                return new UserDTO { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            }

            return user;
        }

        public async Task<UserDTO> PostAsync(RegisterUserDTO obj)
        {
            var errorMessage = await CheckUserData(obj);
            if (errorMessage != null)
            {
                return new UserDTO { ErrorMessage = errorMessage };
            }

            var newUser = obj.GetEntity();

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.CreatedOn = DateTime.Now;

            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();

            return await _db.Users.Include(x => x.ApplicationRole)
                                  .Where(x => x.Username == newUser.Username)
                                  .Select(x => x.GetDTO())
                                  .FirstOrDefaultAsync();

        }
        private async Task<string> CheckUserData(RegisterUserDTO obj)
        {
            if(obj.Username == null || 
                obj.Password == null || 
                obj.Email == null || 
                obj.PhoneNumber == null || 
                obj.FirstName == null || 
                obj.LastName == null)
            {
                return GlobalConstants.INCORRECT_DATA;
            }
            if (!IsValidUser(obj.Username, obj.Email, obj.Password, obj.PhoneNumber))
            {
                return GlobalConstants.INCORRECT_DATA;
            }

            if (await _db.Users.AnyAsync(x => x.Username == obj.Username))
            {
                return GlobalConstants.USERNAME_EXIST;
            }

            if (await _db.Users.AnyAsync(x => x.Email == obj.Email))
            {
                return GlobalConstants.USER_EXISTS;
            }

            if (await _db.Users.AnyAsync(x => x.PhoneNumber == obj.PhoneNumber))
            {
                return GlobalConstants.USER_PHONE_EXISTS;
            }

            return null;
        }


        private bool IsValidUser(string username, string email, string password, string phoneNumber)
        {
            var validUsername = username.Length >= 2 && username.Length <= 20;
            var validEmail = Regex.IsMatch(email ?? "", @"[^@\t\r\n]+@[^@\t\r\n]+\.[^@\t\r\n]+");
            var validPassword = Regex.IsMatch(password ?? "", GlobalConstants.PassRegex);
            var validPhone = Regex.IsMatch(phoneNumber ?? "", GlobalConstants.PhoneRegex);
            return validUsername && validEmail && validPassword && validPhone;
        }
        public async Task<UserDTO> UpdateProfilePicture(string email, string profilePictureURL)
        {
            var user = await _db.Users.Include(x => x.ApplicationRole)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
            {
                return new UserDTO { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            }
            user.ProfilePictureURL = profilePictureURL;
            await _db.SaveChangesAsync();

            return user.GetDTO();
        }
        public async Task<UserDTO> UpdateAsync(string email, UserDTO obj)
        {
            if (await _db.Users.Where(x => x.Email != email).FirstOrDefaultAsync(x => x.Email == obj.Email) != null)
            {
                return new UserDTO { ErrorMessage = GlobalConstants.USER_EXISTS };
            }

            var user = await _db.Users.Include(x => x.ApplicationRole)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
            {
                return new UserDTO { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            }

            MapUser(obj, user);
            user.ModifiedOn = DateTime.Now;

            await _db.SaveChangesAsync();

            return user.GetDTO();
        }
        private static void MapUser(UserDTO obj, User user)
        {
            if (obj.Username != null && obj.Username.Length >= 2 && obj.Username.Length <= 20)
            {
                user.Username = obj.Username;
            }

            if (obj.FirstName != null)
            {
                user.FirstName = obj.FirstName;
            }

            if (obj.LastName != null)
            {
                user.LastName = obj.LastName;
            }

            if (obj.Email != null && Regex.IsMatch(obj.Email ?? "", @"[^@\t\r\n]+@[^@\t\r\n]+\.[^@\t\r\n]+"))
            {
                user.Email = obj.Email;
            }

            if (obj.Password != null && Regex.IsMatch(obj.Password ?? "", GlobalConstants.PassRegex))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(obj.Password);
            }

            if (obj.PhoneNumber != null && Regex.IsMatch(obj.PhoneNumber ?? "", GlobalConstants.PhoneRegex))
            {
                user.PhoneNumber = obj.PhoneNumber;
            }
        }

        public async Task<UserDTO> UpdatePasswordAsync(string email, string newPassword)
        {
            var user = await _db.Users.Include(x => x.ApplicationRole).FirstOrDefaultAsync(x => x.Email == email);

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            await _db.SaveChangesAsync();

            return user.GetDTO();
        }

        public async Task<int> UsersCountAsync()
        {
            return await _db.Users.CountAsync();
        }

    }
}
