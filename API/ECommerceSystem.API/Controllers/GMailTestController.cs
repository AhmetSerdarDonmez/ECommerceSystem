using ECommerceSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GMailTestController : Controller
    {
      
    private readonly IGmailService _gmailService;

        public GMailTestController(IGmailService gmailService)
        {
            _gmailService = gmailService;
        }

        [HttpGet("labels")]
        public async Task<IActionResult> GetLabels()
        {
            var labels = await _gmailService.GetUserLabelsAsync();
            return Ok(labels);
        }
    }
}
