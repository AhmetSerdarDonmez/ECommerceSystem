﻿using ECommerceSystem.Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerceSystem.API.Controllers
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
        public async Task<IActionResult> TestAction()
        {
            await _userWriteRepository.AddAsync(new()
            {UserName = "Mehmet", Email = "m.el", PasswordHash = "584", PhoneNumber = "574", IsDeleted = false , RoleId = 1 });
            var count = await _userWriteRepository.SaveAsync();

            return Ok(new { message = "User added successfully", rowsAffected = count });
        }

    }
}
