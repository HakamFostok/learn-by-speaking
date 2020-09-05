using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace LearnBySpeaking.Domain.Core
{
    public class User : IUser
    {
        private readonly IHttpContextAccessor _context;

        public User(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string Name => _context.HttpContext?.User?.Identity?.Name ?? "";

        public IEnumerable<Claim> Claims => _context.HttpContext.User.Claims;

        public bool IsAuthenticated()
        {
            try
            {
                return _context.HttpContext.User.Identity.IsAuthenticated;
            }
            catch
            {
                return false;
            }
        }
    }
}