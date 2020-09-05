using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBySpeaking.Domain.Interfaces.Core
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task UpdateAsync(TEntity obj);
        Task RemoveAsync(int id);
        Task<TEntity> GetByIdAsync(int id);
        Task<IQueryable<TEntity>> GetAllAsync();
    }
}