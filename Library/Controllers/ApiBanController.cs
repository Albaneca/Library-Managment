using Library.API.Attributes;
using Library.Common;
using Library.Services.Contracts;
using Library.Services.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    public class ApiBanController : ControllerBase
    {
        private readonly IBanService _bs;

        public ApiBanController(IBanService bs)
        {
            _bs = bs;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<IEnumerable<DisplayUserDTO>>> GetAllBannedUsersAsync(int page)
        {
            return Ok(await _bs.GetAllBannedUsersAsync(page));
        }


        [HttpPatch("{email}/{days?}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<BanDTO>> BanUserAsync(string email, string reason, DateTime? days)
        {
            var response = await _bs.BanUserAsync(email, reason, days);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return BadRequest(response.ErrorMessage);
        }

        [HttpPatch("{email}/unban")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<BanDTO>> UnbanUserAsync(string email)
        {
            var response = await _bs.UnbanUserAsync(email);

            if (response.ErrorMessage is null)
            {
                return Ok(response.BanRemovedMessage);
            }

            return BadRequest(response.ErrorMessage);
        }
    }
}
