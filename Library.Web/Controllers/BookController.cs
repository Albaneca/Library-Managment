using Library.API.Client;
using Library.Common;
using Library.Services;
using Library.Services.Contracts;
using Library.Web.Models;
using Library.Web.Models.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bs;
        private readonly GetSelectListService _gs;
        private readonly CloudinaryClient _cloudinary;
        public BookController(IBookService bs, GetSelectListService gs, CloudinaryClient cloudinary)
        {
            _bs = bs;
            _gs = gs;
            _cloudinary = cloudinary;
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Details(long id)
        {
            var book = await _bs.GetBookByNameOrIdAsync(id.ToString());

            return View(book);
        }


        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var newViewModel = new BookViewModel();
            return this.BookView(newViewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(BookViewModel model)
        {
            var book = model.GetDTO();
            await _bs.PostAsync(book);

            return this.RedirectToAction("Index", "Book");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var book = await _bs.GetBookByNameOrIdAsync(id.ToString());
            var model = book.GetModel();

            return this.BookView(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(BookViewModel model)
        {
            var book = model.GetDTO();
            await _bs.UpdateAsync(model.Id, book);
            if(model.ProfilePicture != null)
            {
                var url = _cloudinary.UploadBookPhotoAsync(model.ProfilePicture);

                await _bs.UpdatePicture(model.Id, url.Result.Url.ToString());
            }

            return this.RedirectToAction("Index", "Book");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var book = await _bs.GetBookByNameOrIdAsync(id.ToString());
            var model = book.GetModel();

            return this.BookView(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _bs.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.Index));
        }


        private IActionResult BookView(BookViewModel model)
        {
            model.PublishHouses = this.GetHouses();
            model.Authors = this.GetAuthors();

            return this.View(model);
        }
        private SelectList GetHouses()
        {
            var houses = this._gs.GetHouses();
            return new SelectList(houses, "Id", "Name");
        }
        private SelectList GetAuthors()
        {
            var authors = this._gs.GetAuthors();
            return new SelectList(authors, "Id", "Name");
        }
    }
}
