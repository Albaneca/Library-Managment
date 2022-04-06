using Library.Common;
using Library.Data;
using Library.Services.Contracts;
using Library.Services.DTOs.LoanDTOs;
using Library.Services.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public class LoanService : ILoanService
    {
        private readonly LibraryDbContext _db;
        public LoanService(LibraryDbContext db)
        {
            _db = db;
        }
        public async Task<DisplayLoanDTO> DeleteAsync(long id)
        {
            var loan = await _db.Loans
                               .FirstOrDefaultAsync(x => x.Id == id);

            if (loan is null)
            {
                return new DisplayLoanDTO { ErrorMessage = GlobalConstants.LOAN_NOT_FOUND };
            }

            var loanDTO = loan.GetDTO();

            _db.Loans.Remove(loan);
            await _db.SaveChangesAsync();

            return loanDTO;
        }

        public async Task<IEnumerable<DisplayLoanDTO>> GetAsync(int page)
        {
            return await _db.Loans
                 .Include(l => l.Book).ThenInclude(b => b.Author)
                 .Include(l => l.Book).ThenInclude(b => b.PublishHouse)
                 .Include(l => l.Approver)
                 .Include(l => l.Requester)
                 .Skip(page * GlobalConstants.PageSkip)
                 .Take(10)
                 .OrderBy(x => x.Id)
                 .Select(x => x.GetDTO())
                 .ToListAsync();
        }

        public async Task<IEnumerable<DisplayLoanDTO>> GetNotConfirmedAsync(int page)
        {
            return await _db.Loans
                 .Include(l => l.Book).ThenInclude(b => b.Author)
                 .Include(l => l.Book).ThenInclude(b => b.PublishHouse)
                 .Include(l => l.Approver)
                 .Include(l => l.Requester)
                 .Where(x => x.Status == GlobalConstants.LOAN_NOT_CONFIRMED)
                 .Skip(page * GlobalConstants.PageSkip)
                 .Take(10)
                 .Select(x => x.GetDTO())
                 .ToListAsync();
        }

        public async Task<IEnumerable<DisplayLoanDTO>> GetLoansByRequesterNameOrIdAsync(string nameOrId, int page)
        {
            var loans = await _db.Loans.Include(l => l.Book).ThenInclude(b => b.Author)
                 .Include(l => l.Book).ThenInclude(b => b.PublishHouse)
                 .Include(l => l.Approver)
                 .Include(l => l.Requester)
                 .Where(x => x.Requester.FirstName == nameOrId 
                 || x.Requester.LastName == nameOrId
                 || x.Requester.Username == nameOrId
                 || x.Id.ToString() == nameOrId)
                 .Skip(page * GlobalConstants.PageSkip)
                 .Take(10)
                 .Select(x => x.GetDTO())
                 .ToListAsync();

            return loans;
        }
        public async Task<DisplayLoanDTO> DecideLoan(string email, long id, bool answer)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
                

            if(user is null)
            {
                return new DisplayLoanDTO { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            }

            var loan = await _db.Loans
                .Include(x => x.Approver)
                .Include(x => x.Book)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (loan is null)
            {
                return new DisplayLoanDTO { ErrorMessage = GlobalConstants.LOAN_NOT_FOUND };
            }

            if(answer)
            {
                loan.Status = GlobalConstants.LOAN_CONFIRMED;
                loan.ModifiedOn = DateTime.Now;
                loan.ApproverId = user.Id;
                loan.DueDate = DateTime.Now.AddDays(7);
            }
            else
            {
                loan.Status = GlobalConstants.LOAN_DENIED;
            }
            await _db.SaveChangesAsync();

            return await _db.Loans.Where(x => x.Id == id)
                                  .Select(x => x.GetDTO())
                                  .FirstOrDefaultAsync();
        }
        public async Task<DisplayLoanDTO> PostAsync(string email, CreateLoanDTO obj)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user is null)
            {
                return new DisplayLoanDTO { ErrorMessage = GlobalConstants.USER_NOT_FOUND };
            }

            var newLoan = obj.GetEntity();
            newLoan.RequesterId = user.Id;
            newLoan.CreatedOn = DateTime.Now;
            await _db.Loans.AddAsync(newLoan);
            await _db.SaveChangesAsync();

            var lastLoan = _db.Loans.Count();

            return await _db.Loans.Where(x => x.Id == lastLoan)
                                  .Include(x=> x.Book)
                                  .Include(x => x.Approver)
                                  .Select(x => x.GetDTO())
                                  .FirstOrDefaultAsync();
        }
        public async Task<int> LoanPerUserCountAsync(long id)
        {
            return await _db.Loans.Where(x => x.RequesterId == id).CountAsync();
        }

        public async Task<int> LoansCountAsync()
        {
            return await _db.Loans.CountAsync();
        }
    }
}
