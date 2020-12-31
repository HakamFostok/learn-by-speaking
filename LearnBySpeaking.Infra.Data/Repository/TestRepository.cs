using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using LearnBySpeaking.Domain.Models;
using LearnBySpeaking.Infra.Data.Context;
using LearnBySpeaking.Infra.Data.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBySpeaking.Infra.Data.Repository
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(LearnBySpeakingContext context)
            : base(context)
        {
        }

        public override async Task<Test> GetByIdAsync(int id)
        {
            return await DbSet.Where(x => x.Id == id)
                .Include(x => x.Topic)
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .FirstOrDefaultAsync();
        }

    }
}