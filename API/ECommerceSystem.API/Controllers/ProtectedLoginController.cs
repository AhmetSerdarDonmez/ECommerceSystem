using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires authentication for the entire controller
    public class ProtectedController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProtectedData()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var username = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
 //           var roleId = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
 //           var roles = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value);
            var roleId = User.FindFirst("roleId")?.Value;

            return Ok(new
            {
                Message = "This is protected data!",
                UserId = userId,
                Username = username,
                RoleId = roleId,
 //               RoleIdClaim = roleIdClaim
            });
        }

        [HttpGet("admin")]
        [Authorize(Policy = "AdminOnly")] // Uses the custom "AdminOnly" policy
        public IActionResult GetAdminData()
        {
            return Ok(new { Message = "This is admin data!" });
        }
    }
}