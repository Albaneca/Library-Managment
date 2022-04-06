using Library.API.Attributes;
using Library.API.Client;
using Library.Common;
using Library.Services.Contracts;
using Library.Services.DTOs;
using Library.Services.DTOs.BookDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBookController : ControllerBase
    {
        private readonly IBookService _bs;
        private readonly CloudinaryClient _cloudinary;

        public ApiBookController(IBookService bs, CloudinaryClient cloudinary)
        {
            _bs = bs;
            _cloudinary = cloudinary;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DisplayBookDTO>>> GetAsync(int page)
        {
            return Ok(await _bs.GetAsync(page));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<DisplayBookDTO>> CreateBook(CreateBookDTO obj)
        {
            var response = await _bs.PostAsync(obj);

            if (response.ErrorMessage is null)
            {
                return Created("Get", response);
            }

            return BadRequest(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<DisplayBookDTO>> UpdateAsync(long id, CreateBookDTO obj)
        {

            var response = await _bs.UpdateAsync(id, obj);

            if (response.ErrorMessage is null)
            {
                return Created("Get", response);
            }

            return BadRequest(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<DisplayBookDTO>> DeleteAsync(long id)
        {
            var response = await _bs.DeleteAsync(id);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return NotFound(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpPost("update/picture")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<DisplayBookDTO>> UpdatePicture(long bookId ,IFormFile picture)
        {
            var url = _cloudinary.UploadBookPhotoAsync(picture);

            var response = await _bs.UpdatePicture(bookId, url.Result.Url.ToString());

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
