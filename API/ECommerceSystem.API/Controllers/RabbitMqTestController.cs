using ECommerceSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RabbitMqTestController : ControllerBase
    {
        private readonly IMessagingService _messagingService;
        private readonly IConfiguration _configuration;

        public RabbitMqTestController(IMessagingService messagingService, IConfiguration configuration)
        {
            _messagingService = messagingService;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] EMailModel mail)
        {
            // Validate and process the order (e.g., save to database in Application layer)
            // After processing, publish a message to RabbitMQ for further asynchronous processing.

            var queueName = _configuration["RabbitMQ:QueueName"];
            _messagingService.Publish(mail, queueName);

            return Accepted(new { Message = "Order received and processing initiated." });
        }


        [HttpPost("sendBulk")]
        public async Task<IActionResult> SendBulkMessages(int count)
        {
            var queueName = _configuration["RabbitMQ:QueueName"];
            for (int i = 1; i <= count; i++)
            {
                var message = new { Id = i, Content = $"Test message {i}" };
                _messagingService.Publish(message, queueName);
            }

            return Ok(new { Message = $"{count} messages have been published." });
        }

    }


}
