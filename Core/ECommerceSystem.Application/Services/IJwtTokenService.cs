using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Application.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string username, string roleId);

    }
}
