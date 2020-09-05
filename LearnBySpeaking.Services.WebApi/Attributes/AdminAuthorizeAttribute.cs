using Microsoft.AspNetCore.Authorization;

namespace LearnBySpeaking.Services.WebApi.Attributes
{
    public sealed class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        public AdminAuthorizeAttribute()
        {
            Policy = Domain.Interfaces.EntityInterfaces.UserRolesExtension.ADMIN_POLICY;
        }
    }
}