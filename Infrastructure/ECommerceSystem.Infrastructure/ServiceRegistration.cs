using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application;
using ECommerceSystem.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceSystem.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService , PaymentService>();

        }
    }
}
