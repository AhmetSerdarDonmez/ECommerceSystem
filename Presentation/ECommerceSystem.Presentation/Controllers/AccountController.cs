using Microsoft.AspNetCore.Mvc;
using ECommerceSystem.Presentation.ViewModels;
using ECommerceSystem.Application.Repositories;
using System.Threading.Tasks;

namespace ECommerceSystem.Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        readonly private IUserReadRepository _userReadRepository;
        readonly private IUserWriteRepository _userWriteRepository;

        public RegisterController(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Add your registration logic here

                await _userWriteRepository.AddAsync(new() { UserName = "", PhoneNumber = "5445311034", UserId = 1, Email = model.Email, PasswordHash = model.Password });

                await _userWriteRepository.SaveAsync();



                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

    }




    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Add your login logic here
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Add your registration logic here



                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
