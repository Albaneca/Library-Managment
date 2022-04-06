using Library.Common;
using Library.Services.Contracts;
using Library.Services.DTOs;
using Library.Web.Extensions;
using Library.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    public class BanController : Controller
    {
        private readonly IBanService _ban;
        private readonly IUserService _us;
        private readonly IMailService _ms;

        public BanController(IBanService ban, IUserService us, IMailService ms)
        {
            this._ban = ban;
            this._us = us;
            this._ms = ms;
        }

        public async Task<IActionResult> Index()
        {
            var reported = await _ban.GetAllBannedUsersAsync(0);

            return this.View(reported);
        }


        [HttpPost]
        public async Task<IActionResult> Banned(int p)
        {
            var banned = await _ban.GetAllBannedUsersAsync(p);
            var model = new BanViewModel
            {
                Banned = banned,
                CurrentPage = p,
                MaxPages = await _ban.GetMaxPageAsync()
            };
            return View("_Banned", model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> BanForm(long id)
        {
            var user = await _us.GetUserByEmailOrIdAsync(id.ToString());

            var model = new CreateBanDTO
            {
                Email = user.Email
            };

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Ban()
        {
            return View("BanForm", new CreateBanDTO { isSent = true});
        }

        [HttpPost]
        public async Task<IActionResult> BanForm(CreateBanDTO model)
        {
            await _ban.BanUserAsync(model.Email, model.Reason, model.Days);
            
            var message = $"Due: {model.Days} Reason: {model.Reason}";
            await _ms.SendEmailAsync(new MailDTO { IsBan = true, Reciever = model.Email, Message = message });

            return RedirectToAction(nameof(this.Ban));
        }

        [HttpGet]
        public async Task<IActionResult> Unban(long id)
        {
            var user = await _us.GetUserByEmailOrIdAsync(id.ToString());

            await _ban.UnbanUserAsync(user.Email);

            var users = await _us.FilterUsersAsync(0, "@");

            return RedirectToAction("Index", "User", users);
        }
    }
}
