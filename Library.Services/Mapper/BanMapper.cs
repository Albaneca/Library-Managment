using Library.Data.Models;
using Library.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Mapper
{
    public static class BanMapper
    {
        public static BanDTO GetBanDTO(this User user)
        {
            return new BanDTO
            {
                Picture = user.ProfilePictureURL,
                BlockedDue = user.Ban?.BlockedDue,
                UserEmail = user.Email,
                BlockedOn = user.Ban?.BlockedOn,
                Reason = user.Ban?.Reason
            };
        }
    }
}
