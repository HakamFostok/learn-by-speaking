using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace LearnBySpeaking.Services.WebApi.AuthorizationRequirements
{
    public class UserAuthorizationRequirement : IAuthorizationRequirement
    {
    }

    public class UserAuthorizationRequirementHandler : AuthorizationHandler<UserAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAuthorizationRequirement requirement)
        {
            bool hasRole = context.User.Claims.HasAnyRole(
                UserRolesExtension.ADMIN_ROLE,
                UserRolesExtension.USER_ROLE);

            if (hasRole)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}