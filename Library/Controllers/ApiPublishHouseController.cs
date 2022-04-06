using Library.API.Attributes;
using Library.API.Client;
using Library.Common;
using Library.Services.Contracts;
using Library.Services.DTOs.PublishHouseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPublishHouseController : ControllerBase
    {
        private readonly IPublishHouseService _hs;
        private readonly CloudinaryClient _cloudinary;

        public ApiPublishHouseController(IPublishHouseService hs, CloudinaryClient cloudinary)
        {
            _hs = hs;
            _cloudinary = cloudinary;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DisplayPublishHouseDTO>>> GetAsync(int page)
        {
            return Ok(await _hs.GetAsync(page));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<DisplayPublishHouseDTO>> CreateHouse(CreatePublishHouseDTO obj)
        {
            var response = await _hs.PostAsync(obj);

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
        public async Task<ActionResult<DisplayPublishHouseDTO>> UpdateAsync(long id, CreatePublishHouseDTO obj)
        {

            var response = await _hs.UpdateAsync(id, obj);

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
        public async Task<ActionResult<DisplayPublishHouseDTO>> DeleteAsync(long id)
        {
            var response = await _hs.DeleteAsync(id);

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
        public async Task<ActionResult<DisplayPublishHouseDTO>> UpdatePicture(long houseId, IFormFile picture)
        {
            var url = _cloudinary.UploadHousePhotoAsync(picture);

            var response = await _hs.UpdatePicture(houseId, url.Result.Url.ToString());

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
