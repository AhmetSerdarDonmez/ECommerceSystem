using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ECommerceSystem.Application.Repositories;

namespace ECommerceSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires authentication for the entire controller
    public class ProtectedController : ControllerBase
    {
        private readonly IUserReadRepository _userService; // Inject the UserService

        public ProtectedController(IUserReadRepository userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetProtectedData()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var username = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var roleId = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if(roleId=="1")
            {
                return Ok(new
                {
                    Message = "This is protected data!",
                    UserId = userId,
                    Username = username,
                    RoleId = roleId,
                });
            }
            else
            {
                return Unauthorized("You do not have the required role to access this resource.");
            }


        }

        [HttpGet("admin/{id}")] // Route with an 'id' parameter
        [Authorize(Policy = "AdminLoginWithId")]
        public async Task<IActionResult> GetAdminData(int id)
        {
            // Check if the current user has roleId = 1 (Admin)
            var roleIdClaim = User.FindFirst("roleId")?.Value;
            if (roleIdClaim != null && int.TryParse(roleIdClaim, out int roleId) && roleId == 1)
            {
                var userDto = await _userService.GetByIdAsync(id.ToString());

                if (userDto == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                return Ok(userDto);
            }
            else
            {
                return Forbid("You do not have the required role to access this resource.");
            }
        }
    }
}