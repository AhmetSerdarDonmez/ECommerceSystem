﻿using Microsoft.AspNetCore.Mvc;
using ECommerceSystem.Application.Repositories;
using ECommerceSystem.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using ECommerceSystem.Application.Services;


namespace ECommerceSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly private IUserReadRepository _userReadRepository;
        readonly private IUserWriteRepository _userWriteRepository;
        private readonly IMessagingService _messagingService;
        private readonly IConfiguration _configuration;

        public UserController(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository , IMessagingService messagingService ,IConfiguration configuration)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
            _messagingService = messagingService;
            _configuration = configuration;


        }

        [HttpGet("get-all-users")]
        [Authorize]

        public IActionResult GetAllUsersAction()
        {
            if(User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value != "1")
            {
                return Unauthorized("You do not have the required role to access this resource.");
            }
            var users = _userReadRepository.GetAll().ToList();
            return Ok(users);

        }

        [HttpGet("get-user-by-id/{id}")]
        public async Task<IActionResult> GetUserByIdAction(string id)
        {
            var user = await _userReadRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

                return Ok(user);
        }




        [HttpPost("add-single-user")]

        public async Task<IActionResult> AddSingleUserAction([FromBody]User user)
        {

            var isAdded = await _userWriteRepository.AddAsync(user);

            if (!isAdded)
            {
                return BadRequest("User could not be added");
            }

            await _userWriteRepository.SaveAsync();

            // Create an email event to be sent (you might add more properties if needed)
            var emailEvent = new EMailModel
            {
                receptor = user.Email, // Assuming your User entity has an Email property
                subject = "ETicaret Sisteme Hoşgeldiniz",
                body = $"Kayıt olduğun için teşekkürler {user.UserName} .<br/><br/> " +
                "Seni aramızda görmekten mutluluk duyuyoruz"
            };

            // Publish the email event to the queue
            var queueName = _configuration["RabbitMQ:QueueName"];
            _messagingService.Publish(emailEvent, queueName);

            return Ok(user);
        }

        


        [HttpPost("add-range-user")]
        public async Task<IActionResult> AddRangeOfUsersAction([FromBody] List<User> users)
        {
            var isAdded = await _userWriteRepository.AddRangeAsync(users);
            if (!isAdded)
            {
                return BadRequest("Users could not be added");
            }
            await _userWriteRepository.SaveAsync();
            return Ok(users);
        }

        [HttpPut("update-user")]

        public async Task<IActionResult> UpdateUserAction([FromBody] User user)
        {
            var isUpdated = _userWriteRepository.Update(user);
            if (!isUpdated)
            {
                return BadRequest("User could not be updated");
            }
            await _userWriteRepository.SaveAsync();
            return Ok(user);
        }

        [HttpDelete("remove-single-user")]

        public async Task<IActionResult> RemoveUserAction([FromBody] User user)
        {
            var isRemoved = _userWriteRepository.Remove(user);

            if (!isRemoved)
            {
                return BadRequest("User could not be removed");
            }
            await _userWriteRepository.SaveAsync();
            return Ok(user);
        }

        [HttpDelete("remove-range-user")]           

        public async Task<IActionResult> RemoveRangeOfUsersAction([FromBody] List<User> users)
        {
            var isRemoved = _userWriteRepository.RemoveRange(users);
            if (!isRemoved)
            {
                return BadRequest("Users could not be removed");
            }
            await _userWriteRepository.SaveAsync();
            return Ok(users);
        }

        [HttpDelete("remove-user-by-id/{id}")]

        public async Task<IActionResult> RemoveUserByIdAction(string id)
        {
            var isRemoved = await _userWriteRepository.RemoveAsync(id);
            if (!isRemoved)
            {
                return BadRequest("User could not be removed");
            }
            await _userWriteRepository.SaveAsync();
            return Ok(id);
        }







    }
}
