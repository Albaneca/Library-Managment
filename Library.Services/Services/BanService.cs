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
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public class BanService : IBanService
    {
        private readonly LibraryDbContext _db;

        public BanService(LibraryDbContext db)
        {
            _db = db;
        }

        public async Task<BanDTO> BanUserAsync(string email, string reason, DateTime? days)
        {
            if (string.IsNullOrEmpty(reason) || string.IsNullOrWhiteSpace(reason))
                reason = GlobalConstants.NO_COMMENT;

            var user = await _db.Users
                .Include(x => x.Ban)
               .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
                return new BanDTO() { ErrorMessage = GlobalConstants.USER_NOT_FOUND };

            if(user.Ban == null)
            {
                user.Ban = new Ban();
            }
            user.Ban.BlockedOn = DateTime.UtcNow.Date;
            user.Ban.Reason = reason;
            user.ApplicationRoleId = 3;

            if (days != null)
            {
                user.Ban.BlockedDue = days;
            }
            //if days are empty, null or whitespace ban will be permanent

            await _db.SaveChangesAsync();
            return new BanDTO
            {
                Id = user.Ban.Id,
                UserId = user.Id,
                UserEmail = user.Email,
                BlockedOn = user.Ban.BlockedOn,
                BlockedDue = user.Ban.BlockedDue,
                Reason = reason
            };
        }

        public async Task<IEnumerable<BanDTO>> GetAllBannedUsersAsync(int page)
        {
            return await _db.Users
                .Include(x => x.Ban)
                .Where(x => x.ApplicationRoleId == 3)
                .Skip(page * GlobalConstants.PageSkip)
                .Take(10)
                .Select(x => x.GetBanDTO())
                .ToListAsync();
        }

        public async Task<BanDTO> UnbanUserAsync(string email)
        {
            var user = await _db.Users.Include(x => x.Ban)
               .FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
                return new BanDTO() { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            var ban = await _db.Bans.FirstOrDefaultAsync(x => x.UserId == user.Id);

            user.Ban.BlockedOn = null;
            user.Ban.BlockedDue = null;
            user.ApplicationRoleId = 2;

            await _db.SaveChangesAsync();

            return new BanDTO() { UserId = user.Id, BanRemovedMessage = string.Format(GlobalConstants.USER_UNBLOCKED, $"{user.Email}") };
        }

        public async Task<int> GetMaxPageAsync()
        {
            var count = await this._db.Bans.CountAsync();
            var page = count / GlobalConstants.PageSkip;
            return page;
        }
    }
}
