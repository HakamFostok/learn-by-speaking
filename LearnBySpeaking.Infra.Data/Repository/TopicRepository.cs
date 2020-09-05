using LearnBySpeaking.Domain.Interfaces.EntityInterfaces;
using LearnBySpeaking.Domain.Models;
using LearnBySpeaking.Infra.Data.Context;
using LearnBySpeaking.Infra.Data.Repository.Core;

namespace LearnBySpeaking.Infra.Data.Repository
{
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        public TopicRepository(LearnBySpeakingContext context)
            : base(context)
        {
        }
    }
}