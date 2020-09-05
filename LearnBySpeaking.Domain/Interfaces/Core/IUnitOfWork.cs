using System;
using System.Threading.Tasks;

namespace LearnBySpeaking.Domain.Interfaces.Core
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();

        Task StartTransactionAsync();
        Task RollbackTransactionAsync();
        Task CommitTransactionAsync();
    }
}