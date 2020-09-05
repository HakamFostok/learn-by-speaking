using LearnBySpeaking.Domain.Core;
using LearnBySpeaking.Domain.Interfaces.Core;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LearnBySpeaking.Infra.Data.Repository.Core
{
    public class MongoBaseRepository : IMongoBaseRepository
    {
        private readonly IMongoDatabase _database;
        private readonly IMongodbDatabaseSettings _settings;

        private IMongoCollection<TModel> Model<TModel>() =>
            _database.GetCollection<TModel>(_settings.EventCollectionName);

        public MongoBaseRepository(IMongodbDatabaseSettings settings)
        {
            _settings = settings;
            MongoClient client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public async Task<IQueryable<TModel>> GetList<TModel>(Expression<Func<TModel, bool>> filter = null) where TModel : Event
        {
            return filter == null ?
                (await Model<TModel>().FindAsync(x => true)).ToList().AsQueryable() :
                (await Model<TModel>().FindAsync(filter)).ToList().AsQueryable();
        }

        public async Task<TModel> GetById<TModel>(string id) where TModel : Event =>
            (await Model<TModel>().FindAsync(p => p.MongoId == id)).FirstOrDefault();

        public async Task Create<TModel>(TModel model) where TModel : Event =>
            await Model<TModel>().InsertOneAsync(model);

        public async Task Update<TModel>(string id, TModel model) where TModel : Event =>
            await Model<TModel>().ReplaceOneAsync(p => p.MongoId == id, model);

        public async Task Delete<TModel>(TModel model) where TModel : Event =>
            await Model<TModel>().DeleteOneAsync(p => p.Id == model.Id);

        public async Task Delete<TModel>(string id) where TModel : Event =>
            await Model<TModel>().DeleteOneAsync(p => p.MongoId == id);
    }
}