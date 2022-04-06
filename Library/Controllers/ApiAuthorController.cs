using Library.API.Attributes;
using Library.API.Client;
using Library.Common;
using Library.Services.Contracts;
using Library.Services.DTOs.AuthorDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAuthorController : ControllerBase
    {
        private readonly IAuthorService _aus;
        private readonly CloudinaryClient _cloudinary;

        public ApiAuthorController(IAuthorService aus, CloudinaryClient cloudinary)
        {
            _aus = aus;
            _cloudinary = cloudinary;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DisplayAuthorDTO>>> GetAsync(int page)
        {
            return Ok(await _aus.GetAsync(page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<DisplayAuthorDTO>> GetAuthorById(string id)
        {
            var response = await _aus.GetAuthorById(id);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return NotFound(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DisplayAuthorDTO>>> FilterAuthorsAsync(int page, string part)
        {
            return Ok(await _aus.FilterAuthorsAsync(page, part));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<DisplayAuthorDTO>> CreateAuthor(CreateAuthorDTO obj)
        {
            var response = await _aus.PostAsync(obj);

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
        public async Task<ActionResult<DisplayAuthorDTO>> UpdateAsync(long id, CreateAuthorDTO obj)
        {

            var response = await _aus.UpdateAsync(id, obj);

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
        public async Task<ActionResult<DisplayAuthorDTO>> DeleteAsync(long id)
        {
            var response = await _aus.DeleteAsync(id);

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
        public async Task<ActionResult<DisplayAuthorDTO>> UpdatePicture(long authorId, IFormFile picture)
        {
            var url = _cloudinary.UploadAuthorPhotoAsync(picture);

            var response = await _aus.UpdatePicture(authorId, url.Result.Url.ToString());

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
