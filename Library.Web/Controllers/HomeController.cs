using Library.Common;
using Library.Common.Exceptions;
using Library.Services.Contracts;
using Library.Services.DTOs;
using Library.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _ms;
        private readonly IUserService _us;
        private readonly IBookService _bs;

        public HomeController(IMailService ms,
            IUserService us,
            IBookService bs)
        {
            _ms = ms;
            _us = us;
            _bs = bs;
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var usersCount = await _us.UsersCountAsync();
            var booksCount = await _bs.BooksCountAsync();
            var model = new StatisticsViewModel
            {
                UsersCount = usersCount,
                BooksCount = booksCount
            };
            return View(model);
        }

        public IActionResult Contact()
        {
            return View(new MailDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Contact(MailDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Phone is null)
            {
                model.Phone = GlobalConstants.NOT_PROVIDED;
            }
            model.isFromContact = true;
            await _ms.SendEmailAsync(model);

            model.isSent = true;
            return View(model);
        }

        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            var imageLink = $"{GlobalConstants.Domain}/images/";
            //TODO Add images
            if (exception != null)
            {
                switch (exception)
                {
                    case AppException e:
                        // custom application error
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        imageLink += "400.png";
                        break;
                    case UnauthorizedAppException e:
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        imageLink += "401.png";
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        imageLink += "404.png";
                        break;
                    default:
                        // unhandled error
                        imageLink += "500.png";
                        HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
            }
            else
            {
                imageLink += "404.png";
            }

            var statuscode = HttpContext.Response.StatusCode;
            return View(new ErrorViewModel { StatusCode = statuscode, Message = exception?.Message ?? "Wrong Address!", ImageLink = imageLink });
        }
    }
}
