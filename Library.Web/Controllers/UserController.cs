using Library.API.Client;
using Library.Common;
using Library.Services.Contracts;
using Library.Services.DTOs;
using Library.Web.Models;
using Library.Web.Models.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _us;
        private readonly CloudinaryClient _cloudinary;
        public UserController(IUserService us, CloudinaryClient cloudinary)
        {
            _us = us;
            _cloudinary = cloudinary;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _us.GetUserByEmailOrIdAsync(email);

            var model = user.GetDTO();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(SettingsViewModel model)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await _us.GetUserByEmailOrIdAsync(email);

            if(model.ProfilePicture != null)
            {
                var url = _cloudinary.UploadProfilePhotoAsync(model.ProfilePicture);

                await _us.UpdateProfilePicture(email, url.Result.Url.ToString());
            }

            var modelToDTO = model.GetDTO();

            if (modelToDTO != null)
            {
                await _us.UpdateAsync(email, modelToDTO);
                //TODO Some notification if its succesfull or not?
                if(user.Password == model.Password)
                {
                    await _us.UpdatePasswordAsync(email, model.NewPassword);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Index(string part = "@", int? currentpage = 1)
        {
            ViewData["TotalPages"] = await _us.UsersCountAsync() / 10;
            ViewData["CurrentFilter"] = part;

            if (currentpage.HasValue)
            {
                if (currentpage.Value > 0) currentpage--; else { currentpage = 0; }
            }
            var users = await _us.FilterUsersAsync(currentpage ?? 0, part);

            ViewData["CurrentPage"] = currentpage + 1;
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Details(long id)
        {
            var user = await _us.GetUserByEmailOrIdAsync(id.ToString());

            var model = user.GetDTO();

            return View(model);
        }
    }
}
