using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application.Services;
using ECommerceSystem.Domain;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace ECommerceSystem.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;

        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["StipeSettings:SecretKey"];
        }

        public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string currency, string token)
        {
            var options = new ChargeCreateOptions
            {
                Amount = (long)(amount * 100),
                Currency = currency,
                Source = token,
            };

            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);

            return new PaymentResult
            {
                IsSuccessful = charge.Status == "succeeded",
                PaymentId = charge.Id,
                Amount = charge.Amount / 100.0m,
                Currency = charge.Currency
            };
        }
    }
}