
using ECommerceSystem.Infrastructure.Authorization.PolicyRequirements;

namespace ECommerceSystem.API.Extensions.AuthorizationPolicies
{
    public static class AdminAuthorizationPolicy
    {

        public static void ConfigureAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // This is where you define the "AdminOnly" policy
                options.AddPolicy("AdminLoginWithId", policy =>
                    policy.Requirements.Add(new AdminRolePolicyRequirement()));
            });

        }
    }
}
