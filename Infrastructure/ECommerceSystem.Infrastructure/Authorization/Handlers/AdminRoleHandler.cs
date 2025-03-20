using ECommerceSystem.Application.AuthContracts;
using ECommerceSystem.Application.Interfaces;
using ECommerceSystem.Infrastructure.Authorization.PolicyRequirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerceSystem.Infrastructure.Authorization.Handlers
{
    public class AdminRoleHandler : AuthorizationHandler<AdminRolePolicyRequirement>, IAdminRoleHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminRoleHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRolePolicyRequirement requirement)
        {
            var roleIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("roleId")?.Value;

            if (roleIdClaim != null && int.TryParse(roleIdClaim, out int roleId) && roleId == 1)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}