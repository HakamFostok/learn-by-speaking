using LearnBySpeaking.Domain.Core;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace LearnBySpeaking.Domain.Interfaces.EntityInterfaces
{
    public static class UserRolesExtension
    {
        public const string ADMIN_POLICY = "ADMIN_POLICY";
        public const string USER_POLICY = "USER_POLICY";

        public const string ADMIN_ROLE = "BT.ArchS_ADMIN";
        public const string USER_ROLE = "BT.ArchS_USER";

        public static bool HasAnyRole(this IEnumerable<Claim> claims, params string[] roles)
        {
            if (roles == null || roles.Length == 0)
                return false;

            return roles.Any(x => claims.HasRole(x));
        }

        public static bool HasRole(this IEnumerable<Claim> claims, string role)
        {
            try
            {
                List<string> userRoles = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value.Split(';').ToList();
                return userRoles.Contains(role);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsAdmin(this IUser user) =>
            user.Claims.HasRole(ADMIN_ROLE);

        public static bool IsUser(this IUser user) =>
            user.Claims.HasRole(USER_ROLE);
    }
}