using System.Collections.Generic;
using System.Security.Claims;

namespace LearnBySpeaking.Domain.Core
{
    public interface IUser
    {
        string Name { get; }
        IEnumerable<Claim> Claims { get; }
        bool IsAuthenticated();
    }
}