using Library.Services.Contracts;
using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
	public class AdminController : Controller
	{
		private readonly IUserService _us;
		private readonly IBookService _bs;
		private readonly ILoanService _ls;
		private readonly IAuthorService _as;

		public AdminController(IBookService bs, IUserService us,
			ILoanService ls, IAuthorService authorService)
        {
			_us = us;
			_bs = bs;
			_ls = ls;
			_as = authorService;
        }
		public async Task<IActionResult> Index()
		{
			var totalUsers = await _us.UsersCountAsync();
			var totalBooks = await _bs.BooksCountAsync();
			var totalLoans = await _ls.LoansCountAsync();
			var totalAuthors = await _as.AuthorCountAsync();
			var stats = new StatisticsViewModel
			{
				BooksCount = totalBooks,
				UsersCount = totalUsers,
				LoansCount = totalLoans,
				AuthorsCount = totalAuthors
			};
			return View(stats);
		}
	}
}
