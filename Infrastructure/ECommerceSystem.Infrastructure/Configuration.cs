using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ECommerceSystem.Infrastructure
{
    class Configuration
    {
        public static string ConnectionString

        {
            get
            {
                ConfigurationManager configurationManager = new ConfigurationManager();
                //              string absolutePath = Path.GetFullPath("../../Presentation/ECommerceSystem.Presentation");
                string absolutePath = Path.GetFullPath("../../API/ECommerceSystem.API");

                configurationManager.SetBasePath(absolutePath);
                //configurationManager.SetBasePath("../../Presentation/ECommerceSystem.Presentation");

                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("DefaultConnection");
            }
            
        }
        public static string PublishableKey
        {
            get
            {
                ConfigurationManager configurationManager = new ConfigurationManager();

                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetSection("Stripe")["PublishableKey"];

            }
        }

        public static string SecretKey
        {
            get
            {
                ConfigurationManager configurationManager = new ConfigurationManager();
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetSection("Stripe")["SecretKey"];
            }
        }
    }
}
