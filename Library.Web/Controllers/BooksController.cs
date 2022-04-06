using Library.API.Client;
using Library.Services;
using Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bs;
        public BooksController(IBookService bs)
        {
            _bs = bs;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? currentpage = 1)
        {
            ViewData["TotalPages"] = await _bs.BooksCountAsync() / 10;

            if (currentpage.HasValue)
            {
                if (currentpage.Value > 0) currentpage--; else { currentpage = 0; }
            }
            var books = await _bs.GetAsync(currentpage ?? 0);

            ViewData["CurrentPage"] = currentpage + 1;
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            if (Request.HttpContext.User.Claims.Any())
            {
                var book = await _bs.GetBookByNameOrIdAsync(id.ToString());

                return View(book);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }
    }
}
