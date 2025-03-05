using Microsoft.AspNetCore.Mvc;
using ECommerceSystem.Presentation.ViewModels;

namespace ECommerceSystem.Presentation.Controllers
{
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
