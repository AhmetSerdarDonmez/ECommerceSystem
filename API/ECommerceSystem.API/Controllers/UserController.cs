using Microsoft.AspNetCore.Mvc;
using ECommerceSystem.Application.Repositories;
using ECommerceSystem.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using ECommerceSystem.Application.Services;
using Google.Apis.Auth;
using System.Linq;

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
        private readonly IAuthenticationService _authenticationService;

        public UserController(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository, IMessagingService messagingService, IConfiguration configuration, IAuthenticationService authenticationService)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
            _messagingService = messagingService;
            _configuration = configuration;
            _authenticationService = authenticationService;
        }

        [HttpGet("get-all-users")]
        [Authorize]
        public IActionResult GetAllUsersAction()
        {
            if (User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value != "1")
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
        public async Task<IActionResult> AddSingleUserAction([FromBody] User user)
        {
            var isAdded = await _userWriteRepository.AddAsync(user);
            if (!isAdded)
            {
                return BadRequest("User could not be added");
            }
            await _userWriteRepository.SaveAsync();

            var emailEvent = new EMailModel
            {
                receptor = user.Email,
                subject = "ETicaret Sisteme Hoşgeldiniz",
                body = $"Kayıt olduğun için teşekkürler {user.UserName} .<br/><br/> Seni aramızda görmekten mutluluk duyuyoruz"
            };

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

        // New endpoint for Google Sign-In
        [HttpPost("google-signin")]
        public async Task<IActionResult> GoogleSignInAction([FromBody] GoogleSignInRequest request)
        {
            if (string.IsNullOrEmpty(request.token))
            {
                return BadRequest("Invalid Google token.");
            }

            GoogleJsonWebSignature.Payload payload;
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new List<string> { _configuration["Google:ClientId"] }
                };
                payload = await GoogleJsonWebSignature.ValidateAsync(request.token, settings);
            }
            catch (Exception ex)
            {
                return Unauthorized($"Invalid or expired Google token. Error: {ex.Message}");
            }

            var email = payload.Email;
            var name = payload.Name;
            var googleUserId = payload.Subject;

            // Retrieve existing user by email; ensure you get a single user object.
            var existingUser = _userReadRepository.GetWhere(u => u.Email == email).FirstOrDefault();
            if (existingUser == null)
            {
                var newUser = new User
                {
                    RoleId = 1,
                    Email = email,
                    UserName = name,
                   // PasswordHash = request.token, // Using token as a marker for Google sign-in
                   PasswordHash ="google",
                   PhoneNumber = "---",
                    
                };

                var isAdded = await _userWriteRepository.AddAsync(newUser);
                if (!isAdded)
                {
                    return BadRequest("User could not be added.");
                }
                await _userWriteRepository.SaveAsync();

                var emailEvent = new EMailModel
                {
                    receptor = newUser.Email,
                    subject = "Welcome to our E-Commerce System",
                    body = $"Thank you for registering, {newUser.UserName}! We’re glad to have you with us."
                };
                var queueName = _configuration["RabbitMQ:QueueName"];
                _messagingService.Publish(emailEvent, queueName);

                // Set existingUser to the newly created user so that we return it.
               

             existingUser = newUser;



            }
            
            var existingUserToken =await _authenticationService.GenerateTokenForUserAsync(existingUser);

            if (!existingUserToken.Success || string.IsNullOrEmpty(existingUserToken.Token))
            {
                // Log error: Failed to generate token
                return StatusCode(500, "User processed but failed to generate application token.");
            }


            // Here you could generate and return a JWT/session token if needed.
            // existingUser = await _userReadRepository.GetByIdAsync("16");
          
                return Ok( new { token = existingUserToken.Token });

            

            

        }
    }

    public class GoogleSignInRequest
    {
        public string token { get; set; }
    }
}
