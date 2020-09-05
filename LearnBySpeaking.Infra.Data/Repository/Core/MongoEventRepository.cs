using LearnBySpeaking.Domain.Interfaces.Core;

namespace LearnBySpeaking.Infra.Data.Repository.Core
{
    public class MongoEventRepository : MongoBaseRepository, IMongoEventRepository
    {
        public MongoEventRepository(IMongodbDatabaseSettings settings)
            : base(settings)
        {
        }
    }
}