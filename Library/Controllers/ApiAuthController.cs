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
    public class ApiAuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public ApiAuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<IEnumerable<ResponseAuthDTO>>> Login(RequestAuthDTO model)
        {
            return Ok(await _auth.AuthenticateAsync(model));
        }
    }
}
