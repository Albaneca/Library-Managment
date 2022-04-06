using Library.API.Attributes;
using Library.API.Client;
using Library.Common;
using Library.Services.Contracts;
using Library.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUserController : ControllerBase
    {
        private readonly IUserService _us;
        private readonly CloudinaryClient cloudinary;
        public ApiUserController(IUserService us, CloudinaryClient cloudinary)
        {
            _us = us;
            this.cloudinary = cloudinary;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DisplayUserDTO>>> GetAsync(int page)
        {
            return Ok(await _us.GetAsync(page));
        }

        [HttpGet("filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DisplayUserDTO>>> FilterUsersAsync(int page, string part)
        {
            return Ok(await _us.FilterUsersAsync(page, part));
        }

        [HttpGet("{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<DisplayUserDTO>> GetUserByEmailAsync(string email)
        {
            var response = await _us.GetUserByEmailOrIdAsync(email);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return NotFound(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<DisplayUserDTO>> CreateUser(RegisterUserDTO obj)
        {
            var response = await _us.PostAsync(obj);

            if (response.ErrorMessage is null)
            {
                return Created("Get", response);
            }

            return BadRequest(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<DisplayUserDTO>> UpdateAsync(UserDTO obj)
        {
            var user = HttpContext.Items[GlobalConstants.UserRoleName] as ResponseAuthDTO;

            var response = await _us.UpdateAsync(user.Email, obj);

            if (response.ErrorMessage is null)
            {
                return Created("Get", response);
            }

            return BadRequest(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpPost("update/picture")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<DisplayUserDTO>> UpdateProfilePicture(IFormFile profilePicture)
        {
            var user = HttpContext.Items[GlobalConstants.UserRoleName] as ResponseAuthDTO;

            var url = cloudinary.UploadProfilePhotoAsync(profilePicture);

            var response = await _us.UpdateProfilePicture(user.Email, url.Result.Url.ToString());

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<DisplayUserDTO>> DeleteAsync()
        {
            var user = HttpContext.Items[GlobalConstants.UserRoleName] as ResponseAuthDTO;

            var response = await _us.DeleteAsync(user.Email);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return NotFound(new { ErrorMessage = response.ErrorMessage });
        }
    }

}
