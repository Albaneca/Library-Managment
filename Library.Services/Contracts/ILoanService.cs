using Library.Services.DTOs.LoanDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Contracts
{
    public interface ILoanService
    {
        Task<IEnumerable<DisplayLoanDTO>> GetAsync(int page);
        Task<DisplayLoanDTO> PostAsync(string email, CreateLoanDTO obj);
        Task<DisplayLoanDTO> DeleteAsync(long id);
        Task<IEnumerable<DisplayLoanDTO>> GetNotConfirmedAsync(int page);
        Task<IEnumerable<DisplayLoanDTO>> GetLoansByRequesterNameOrIdAsync(string nameOrId, int page);
        Task<DisplayLoanDTO> DecideLoan(string email, long id, bool answer);
        Task<int> LoanPerUserCountAsync(long id);
        Task<int> LoansCountAsync();
    }
}
