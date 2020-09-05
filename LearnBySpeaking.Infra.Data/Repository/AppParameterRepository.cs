using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using LearnBySpeaking.Domain.Models;
using LearnBySpeaking.Infra.Data.Context;
using LearnBySpeaking.Infra.Data.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LearnBySpeaking.Infra.Data.Repository
{
    public class AppParameterRepository : Repository<AppParameter>, IAppParameterRepository
    {
        public AppParameterRepository(LearnBySpeakingContext context)
            : base(context)
        {
        }

        public async Task<AppParameter> GetAppParameterByNameAsync(string name)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}