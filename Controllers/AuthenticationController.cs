using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartupDemo.Data.Dtos.Request;
using StartupDemoCore.Interfaces;
using System;
using System.Threading.Tasks;

namespace StartupDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authentication;
        public AuthenticationController(IAuthService authentication)
        {
            _authentication = authentication;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserRequestDto userRequest)
        {
            try
            {
                return Ok(await _authentication.Login(userRequest));
            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}