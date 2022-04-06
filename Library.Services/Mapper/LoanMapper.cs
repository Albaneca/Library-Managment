using Library.Common;
using Library.Data.Models;
using Library.Services.DTOs.LoanDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Mapper
{
    public static class LoanMapper
    {
        public static DisplayLoanDTO GetDTO(this Loan loan)
        {
            if (loan == null)
            {
                return new DisplayLoanDTO() { ErrorMessage = GlobalConstants.INCORRECT_DATA };
            }

            return new DisplayLoanDTO()
            {
                Id = loan.Id,
                Status = loan.Status,
                ApproverName = loan.ApproverId == null ? "-" : $"{loan.Approver.FirstName} {loan.Approver.LastName}",
                BookName = loan.Book.Title,
                DueDate = loan.DueDate,
                RequesterName = $"{loan.Requester.FirstName} {loan.Requester.LastName}",
            };
        }

        public static Loan GetEntity(this CreateLoanDTO loan)
        {
            return new Loan()
            {
                BookId = loan.BookId
            };
        }
    }
}
