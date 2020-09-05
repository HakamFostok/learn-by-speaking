using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace LearnBySpeaking.Services.WebApi.AuthorizationRequirements
{
    public class AdminAuthorizationRequirement : IAuthorizationRequirement
    {
    }

    public class AdminAuthorizationRequirementHandler : AuthorizationHandler<AdminAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminAuthorizationRequirement requirement)
        {
            bool isAdmin = context.User.Claims.HasRole(UserRolesExtension.ADMIN_ROLE);
            if (isAdmin)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}