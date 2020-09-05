using LearnBySpeaking.Domain.Interfaces.Core;
using LearnBySpeaking.Domain.Models;
using System.Threading.Tasks;

namespace LearnBySpeaking.Domain.Interfaces.EntityInterfaces
{
    public interface IAppParameterRepository : IRepository<AppParameter>
    {
        Task<AppParameter> GetAppParameterByNameAsync(string name);
    }
}