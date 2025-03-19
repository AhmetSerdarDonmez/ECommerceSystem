using Microsoft.AspNetCore.Mvc;
using ECommerceSystem.Application.Services;
using ECommerceSystem.API.Models;

namespace ECommerceSystem.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationService.AuthenticateAsync(model.Username, model.Password);

            if (result.Success)
            {
                return Ok(new { Token = result.Token });
            }

            return Unauthorized();
        }


    }
}
