using ECommerceSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("charge")]
        public async Task<IActionResult> Charge([FromBody] PaymentRequest request)
        {
            var result = await _paymentService.ProcessPaymentAsync(request.Amount, request.Currency, request.Token);
            if (result.IsSuccessful)
            {
                return Ok(result);
            }
            return BadRequest("Payment failed");
        }
    }
    
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Token { get; set; }
    }
}
