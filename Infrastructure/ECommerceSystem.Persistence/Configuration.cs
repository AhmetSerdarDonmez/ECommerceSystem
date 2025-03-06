using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ECommerceSystem.Persistence
{
    class Configuration
    {
       public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new ConfigurationManager();
                string absolutePath = Path.GetFullPath("../../Presentation/ECommerceSystem.Presentation");
                configurationManager.SetBasePath(absolutePath);
                //configurationManager.SetBasePath("../../Presentation/ECommerceSystem.Presentation");

                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("DefaultConnection");
            }
        }
    }
}
