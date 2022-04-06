using Library.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface IBanService
    {
        Task<IEnumerable<BanDTO>> GetAllBannedUsersAsync(int page);

        Task<BanDTO> BanUserAsync(string email, string reason, DateTime? days);

        Task<BanDTO> UnbanUserAsync(string email);

        Task<int> GetMaxPageAsync();
    }
}
