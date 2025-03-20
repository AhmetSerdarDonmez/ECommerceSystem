using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication; // Add this using directive
using Microsoft.AspNetCore.Authentication.JwtBearer; // Add this using directive
using Microsoft.IdentityModel.Tokens;
using System.IO; // Add this for Path


namespace ECommerceSystem.Infrastructure
{
    static class Configuration
    {
        /*
        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Set to true in production over HTTPS
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Optional: Adjust if needed
                };
            });
        }
        */


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
