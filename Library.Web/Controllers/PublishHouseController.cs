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
    public class PublishHouseController : Controller
    {
        private readonly IPublishHouseService _phs;
        private readonly CloudinaryClient _cloudinary;
        public PublishHouseController(IPublishHouseService phs, CloudinaryClient cloudinary)
        {
            _phs = phs;
            _cloudinary = cloudinary;
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Index(int? currentpage = 0)
        {
            ViewData["TotalPages"] = await _phs.HouseCountAsync() / 10;

            if (currentpage.HasValue)
            {
                if (currentpage.Value > 0) currentpage--; else { currentpage = 0; }
            }
            var houses = await _phs.GetAsync(currentpage ?? 0);

            ViewData["CurrentPage"] = currentpage + 1;
            return View(houses);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(long id)
        {
            var house = await _phs.GetHouseByNameOrIdAsync(id.ToString());

            return View(house);
        }


        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var newViewModel = new PublishHouseViewModel();
            return View(newViewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(PublishHouseViewModel model)
        {
            var house = model.GetDTO();
            await _phs.PostAsync(house);

            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var house = await _phs.GetHouseByNameOrIdAsync(id.ToString());
            var model = house.GetModel();

            return View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(PublishHouseViewModel model)
        {
            var house = model.GetDTO();
            await _phs.UpdateAsync(model.Id, house);
            if (model.ProfilePicture != null)
            {
                var url = _cloudinary.UploadHousePhotoAsync(model.ProfilePicture);

                await _phs.UpdatePicture(model.Id, url.Result.Url.ToString());
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var house = await _phs.GetHouseByNameOrIdAsync(id.ToString());
            var model = house.GetModel();

            return View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _phs.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
