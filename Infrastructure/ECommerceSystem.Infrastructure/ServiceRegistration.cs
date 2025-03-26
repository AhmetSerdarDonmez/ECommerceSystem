using ECommerceSystem.Application.Services;
using ECommerceSystem.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceSystem.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
        {

            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));



            services.AddScoped<IPaymentService , PaymentService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IMessagingService, RabbitMqService>();
            services.AddHostedService<RabbitMqConsumerService>();

            
            


        }
    }
}
