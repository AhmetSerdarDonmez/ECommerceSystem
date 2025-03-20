using Microsoft.AspNetCore.Authorization;
using ECommerceSystem.Application.AuthContracts;


namespace ECommerceSystem.Infrastructure.Authorization.PolicyRequirements
{
    public class AdminRolePolicyRequirement : IAuthorizationRequirement, IAdminRolePolicyRequirement
    {
        public AdminRolePolicyRequirement()
        {

        }
    }
}
