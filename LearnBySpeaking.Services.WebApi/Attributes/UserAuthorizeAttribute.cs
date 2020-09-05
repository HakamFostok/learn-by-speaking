using Microsoft.AspNetCore.Authorization;

namespace LearnBySpeaking.Services.WebApi.Attributes
{
    public sealed class UserAuthorizeAttribute : AuthorizeAttribute
    {
        public UserAuthorizeAttribute()
        {
            Policy = Domain.Interfaces.EntityInterfaces.UserRolesExtension.USER_POLICY;
        }
    }
}