using ECommerceSystem.Application.Services;
using ECommerceSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceSystem.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
        {

            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = configuration["EmailSettings:OAuthClientId"]; ;
                options.ClientSecret = configuration["EmailSettings:OAuthClientSecret"];
            });



            services.AddScoped<IPaymentService , PaymentService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddHostedService<RabbitMqConsumerService>();
            services.AddTransient<IEMailService, EMailService>();
            services.AddTransient<IMessagingService , RabbitMqService>();





        }
    }
}
