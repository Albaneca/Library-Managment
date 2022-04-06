using Library.Common;
using Library.Data.Models;
using Library.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Library.Services.Mapper
{
    public static class UserMapper
    {
        public static UserDTO GetDTO(this User user)
        {
            if (user is null || user.Username is null || user.FirstName is null
                || user.LastName is null || user.Email is null
                || !Regex.IsMatch(user.PhoneNumber ?? "", GlobalConstants.PhoneRegex)
                || !Regex.IsMatch(user.Password ?? "", GlobalConstants.PassRegex))
            {
                return new UserDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsBlocked = user.Ban?.BlockedOn == null ? false : true,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                Role = user.ApplicationRole.Name,
                ImageLink = user.ProfilePictureURL,
                Created = user.CreatedOn,
                Loans = user.Loans.ToList()
            };
        }
        public static User GetEntity(this RegisterUserDTO user)
        {
            return new User
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password
            };
        }

        public static DisplayUserDTO GetDisplayDTO(this User user)
        {
            if (user is null || user.Username is null || user.FirstName is null
                || user.LastName is null || user.Email is null
                || !Regex.IsMatch(user.PhoneNumber ?? "", GlobalConstants.PhoneRegex)
                || !Regex.IsMatch(user.Password ?? "", GlobalConstants.PassRegex))
            {
                return new DisplayUserDTO { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            return new DisplayUserDTO
            {
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsBlocked = user.Ban?.BlockedOn == null ? false : true,
                LoansCount = user.Loans.Count()
            };
        }
    }
}