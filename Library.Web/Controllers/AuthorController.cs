using Library.API.Client;
using Library.Common;
using Library.Services.Contracts;
using Library.Web.Models;
using Library.Web.Models.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _aus;
        private readonly CloudinaryClient _cloudinary;
        public AuthorController(IAuthorService aus, CloudinaryClient cloudinary)
        {
            _aus = aus;
            _cloudinary = cloudinary;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Index(int? currentpage = 1)
        {
            ViewData["TotalPages"] = await _aus.AuthorCountAsync() / 10;

            if (currentpage.HasValue)
            {
                if (currentpage.Value > 0) currentpage--; else { currentpage = 0; }
            }
            var authors = await _aus.GetAsync(currentpage ?? 0);

            ViewData["CurrentPage"] = currentpage + 1;
            return View(authors);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(long id)
        {
            var author = await _aus.GetAuthorById(id.ToString());

            return View(author);
        }


        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var newViewModel = new AuthorViewModel();
            return View(newViewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(AuthorViewModel model)
        {
            var author = model.GetDTO();
            await _aus.PostAsync(author);

            return this.RedirectToAction("Index", "Author");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var author = await _aus.GetAuthorById(id.ToString());
            var model = author.GetModel();

            return View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(AuthorViewModel model)
        {
            var author = model.GetDTO();
            await _aus.UpdateAsync(model.Id, author);
            if (model.ProfilePicture != null)
            {
                var url = _cloudinary.UploadAuthorPhotoAsync(model.ProfilePicture);

                await _aus.UpdatePicture(model.Id, url.Result.Url.ToString());
            }

            return this.RedirectToAction("Index", "Author");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var author = await _aus.GetAuthorById(id.ToString());
            var model = author.GetModel();

            return View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _aus.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
