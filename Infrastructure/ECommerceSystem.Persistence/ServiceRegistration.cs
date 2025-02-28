using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceSystem.Persistence
{
    public static   class ServiceRegistration
    {

        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<>

        }

    }
}
