using Library.Common;
using Library.Services.Contracts;
using Library.Services.DTOs.LoanDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanService _ls;
        private readonly IUserService _us;
        public LoanController(ILoanService ls, IUserService us)
        {
            _ls = ls;
            _us = us;
        }

        [Authorize]
        public async Task<IActionResult> Index(int? currentpage = 0)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _us.GetUserByEmailOrIdAsync(email);

            ViewData["TotalPages"] = (await _ls.LoanPerUserCountAsync(user.Id) - 1) / 10;

            if (currentpage.HasValue)
            {
                if (currentpage.Value > 0) currentpage--; else { currentpage = 0; }
            }
            var loans = await _ls.GetLoansByRequesterNameOrIdAsync(user.Username, currentpage ?? 0);
            ViewData["CurrentPage"] = currentpage + 1;
            return View(loans);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int? currentpage = 1)
        {
            ViewData["TotalPages"] = (await _ls.LoansCountAsync() - 1) / 10;

            if (currentpage.HasValue)
            {
                if (currentpage.Value > 0) currentpage--; else { currentpage = 0; }
            }
            var loans = await _ls.GetAsync(currentpage ?? 0);
            ViewData["CurrentPage"] = currentpage + 1;
            return View(loans);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> NotConfirmed(int? currentpage = 0)
        {
            var loans = await _ls.GetNotConfirmedAsync(currentpage ?? 0);
            ViewData["TotalPages"] = (loans.Count() - 1) / 10;

            if (currentpage.HasValue)
            {
                if (currentpage.Value > 0) currentpage--; else { currentpage = 0; }
            }
            
            ViewData["CurrentPage"] = currentpage + 1;
            return View("GetAll", loans);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> Decide(string answerString, long id)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            bool answer = false;
            if (answerString == "true")
            {
                answer = true;
            }
            var loan = await _ls.DecideLoan(email, id, answer);
            return RedirectToAction("GetAll");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RequestLoan(long id)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            
            var book = new CreateLoanDTO
            {
                BookId = id
            };
            var loan = await _ls.PostAsync(email, book);
            var page = (await _ls.LoansCountAsync() - 1) / 10;
            return RedirectToAction("Index", "Loan", new { currentpage = page + 1 });
        }
    }
}
