using Library.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface IAuthService
    {
        string CheckConfirmTokenAndExtractEmail(string token);

        Task<bool> IsExistingAsync(string email);

        Task<ResponseAuthDTO> AuthenticateAsync(RequestAuthDTO model);

        Task<ResponseAuthDTO> GetByEmailAsync(string email);

        Task<bool> IsPasswordValidAsync(string email, string password);

        Task<string> ConfirmEmail(string token);

        Task<bool> IsEmailValidForPasswordReset(string email);

        Task<long> GetUserId(string email);
    }
}
