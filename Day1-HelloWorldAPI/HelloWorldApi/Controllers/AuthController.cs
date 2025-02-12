using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User request)
        {
            if(request.Name == "SKA97" &&  request.Password == "Bit2isska97")
            {
                var token = _tokenService.GenerateToken(request.Name);
                return Ok(new { Token  = token });
            }
            return Unauthorized();
        }
    }
}
