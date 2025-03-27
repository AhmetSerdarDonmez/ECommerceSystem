    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using ECommerceSystem.Application.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    namespace ECommerceSystem.Infrastructure.Services
    {
        public class JwtTokenService : IJwtTokenService
        {
            private readonly IConfiguration _configuration;

            public JwtTokenService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public string GenerateToken(string userId, string username, string roleId)
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Secret"]!));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // Access the value directly from the section and parse it to an integer
                if (!int.TryParse(jwtSettings["ExpiryInMinutes"], out int tokenLifetimeInMinutes))
                {
                    // Handle the case where the configuration value is missing or not a valid integer
                    throw new Exception("Invalid or missing TokenLifetimeInMinutes configuration.");
                }
                var tokenLifetime = TimeSpan.FromMinutes(tokenLifetimeInMinutes);

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, roleId)
            };

                        

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.Add(tokenLifetime),
                    SigningCredentials = credentials,
                    Issuer = jwtSettings["Issuer"],
                    Audience = jwtSettings["Audience"]
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }