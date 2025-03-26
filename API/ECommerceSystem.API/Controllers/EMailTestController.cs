using ECommerceSystem.API.Models;
using ECommerceSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EMailTestController : ControllerBase
    {
        private readonly IEMailService _EMailService;

        public EMailTestController(IEMailService EMailService)
        {
            _EMailService = EMailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEMail([FromBody] EMailModel mailModel)
        {
            var result = await _EMailService.SendEmailAsync(mailModel.receptor, mailModel.subject, mailModel.body);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest("EMail failed");
        }
   
    }



}
