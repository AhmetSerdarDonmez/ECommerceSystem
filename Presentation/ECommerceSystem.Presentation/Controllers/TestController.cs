using ECommerceSystem.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerceSystem.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        readonly private IUserReadRepository _userReadRepository;
        readonly private IUserWriteRepository _userWriteRepository;

        public TestController(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }

        [HttpGet("Ping")]
        public IActionResult Ping()
        {
            return Ok("Controller is working!");
        }



        [HttpGet]
        public async Task<IActionResult> TestAction() // Changed return type to Task<IActionResult>
        {
            await _userWriteRepository.AddAsync(new()
            { UserId = 1, UserName = "Ahmet", Email = "a.serdar", PasswordHash = "123", PhoneNumber = "544", IsDeleted = false , RoleId = 1 });
            var count = await _userWriteRepository.SaveAsync();

            return Ok(new { message = "User added successfully", rowsAffected = count }); // Return Ok with data
        }

    }
}
