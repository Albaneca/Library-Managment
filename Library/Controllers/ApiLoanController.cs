using Library.API.Attributes;
using Library.API.Client;
using Library.Common;
using Library.Services.Contracts;
using Library.Services.DTOs;
using Library.Services.DTOs.LoanDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiLoanController : ControllerBase
    {
        private readonly ILoanService _ls;

        public ApiLoanController(ILoanService ls)
        {
            _ls = ls;
        }

        [HttpGet("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<IEnumerable<DisplayLoanDTO>>> GetAsync(int page)
        {
            return Ok(await _ls.GetAsync(page));
        }


        [HttpGet("byId")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<IEnumerable<DisplayLoanDTO>>> GetByRequesterNameOrId(string nameOrId, int page)
        {
            return Ok(await _ls.GetLoansByRequesterNameOrIdAsync(nameOrId, page));
        }

        [HttpGet("notconfirmed")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<IEnumerable<DisplayLoanDTO>>> GetNotConfirmedLoans(int page)
        {
            return Ok(await _ls.GetNotConfirmedAsync(page));
        }

        [HttpPost("create")]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<ActionResult<DisplayLoanDTO>> CreateLoan(CreateLoanDTO obj)
        {
            var user = HttpContext.Items[GlobalConstants.UserRoleName] as ResponseAuthDTO;

            var response = await _ls.PostAsync(user.Email, obj);

            if (response.ErrorMessage is null)
            {
                return Created("Get", response);
            }

            return BadRequest(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpPut("answer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<DisplayLoanDTO>> DecideLoan(long id, bool answer)
        {
            var user = HttpContext.Items[GlobalConstants.UserRoleName] as ResponseAuthDTO;

            var response = await _ls.DecideLoan(user.Email, id, answer);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return BadRequest(new { ErrorMessage = response.ErrorMessage });
        }

        [HttpDelete("delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<ActionResult<DisplayLoanDTO>> DeleteAsync(long id)
        {
            var response = await _ls.DeleteAsync(id);

            if (response.ErrorMessage is null)
            {
                return Ok(response);
            }

            return NotFound(new { ErrorMessage = response.ErrorMessage });
        }
    }
}
