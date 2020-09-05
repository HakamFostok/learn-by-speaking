using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using LearnBySpeaking.Domain.Models;
using LearnBySpeaking.Infra.Data.Context;
using LearnBySpeaking.Infra.Data.Repository.Core;

namespace LearnBySpeaking.Infra.Data.Repository
{
    public class TestRepository : Repository<Test>, ITestRepository
    {
        public TestRepository(LearnBySpeakingContext context)
            : base(context)
        {
        }
    }
}