using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Users;

namespace ECommerceSystem.Application.Services
{
    public interface IAuthenticationService
    {
        Task<(bool Success, string? Token)> AuthenticateAsync(string username, string password);

        Task<(bool Success, string? Token)> GenerateTokenForUserAsync(User user);


    }
}
