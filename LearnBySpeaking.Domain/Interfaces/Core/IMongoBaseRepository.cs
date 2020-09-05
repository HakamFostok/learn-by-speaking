using LearnBySpeaking.Domain.Core;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LearnBySpeaking.Domain.Interfaces.Core
{
    public interface IMongoBaseRepository
    {
        Task<IQueryable<TModel>> GetList<TModel>(Expression<Func<TModel, bool>> filter = null)
            where TModel : Event;

        Task<TModel> GetById<TModel>(string id)
            where TModel : Event;

        Task Create<TModel>(TModel model)
            where TModel : Event;

        Task Update<TModel>(string id, TModel model)
            where TModel : Event;

        Task Delete<TModel>(TModel model)
            where TModel : Event;

        Task Delete<TModel>(string id)
            where TModel : Event;
    }
}