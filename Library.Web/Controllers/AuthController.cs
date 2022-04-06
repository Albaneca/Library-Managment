using Library.Common;
using Library.Services.Contracts;
using Library.Services.DTOs;
using Library.Services.Mapper;
using Library.Web.Models;
using Library.Web.Models.Mappers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _auth;
        private readonly IUserService _us;
        private readonly IMailService _mail;

        public AuthController(IAuthService auth,
            IUserService us,
            IMailService mail)
        {
            _auth = auth;
            _us = us;
            _mail = mail;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new RequestAuthDTO());
        }


        [HttpPost]
        public async Task<IActionResult> Login(RequestAuthDTO model)
        {
            if (!await _auth.IsPasswordValidAsync(model.Email, model.Password))
            {
                ModelState.AddModelError("Password", GlobalConstants.WRONG_CREDENTIALS);
                return View(model);
            }
            var user = await _auth.GetByEmailAsync(model.Email);

            if (user.Message != null)
            {
                ModelState.AddModelError("Password", GlobalConstants.USER_BLOCKED_JOIN);
                return View(model);
            }

            await SignInWithRoleAsync(model.Email, user.Role);

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            var email = await _auth.ConfirmEmail(token);
            if (email != null)
            {
                await SignInWithRoleAsync(email, GlobalConstants.UserRoleName);

                return RedirectToAction("index", "home");
            }

            return RedirectToAction("error", "home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (await _auth.IsExistingAsync(model.Email))
            {
                ModelState.AddModelError("Email", GlobalConstants.USER_EXISTS);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var toUser = model.GetDTO();

            await _us.PostAsync(toUser);

            await _mail.SendEmailAsync(new MailDTO { Reciever = model.Email });

            ViewData["MessageSent"] = true;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (await _auth.IsEmailValidForPasswordReset(email))
            {
                await _mail.SendEmailAsync(new MailDTO { Reciever = email, ResetPassword = true });
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult UpdatePassword(string token)
        {
            var email = _auth.CheckConfirmTokenAndExtractEmail(token);
            if (email.Contains('@'))
            {
                TempData["Email"] = email;
                return View(new UpdatePasswordViewModel());
            }
            return RedirectToAction("error", "home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel obj)
        {
            var email = TempData["Email"].ToString();
            await _us.UpdatePasswordAsync(email, obj.Password);
            ViewData["PasswordUpdated"] = true;
            return View(new UpdatePasswordViewModel());
        }

        private async Task SignInWithRoleAsync(string email, string userRoleName)
        {
            //You can add more claims as you wish but keep these KEYS here as is
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Email, email));
            identity.AddClaim(new Claim(ClaimTypes.Role, userRoleName));


            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
