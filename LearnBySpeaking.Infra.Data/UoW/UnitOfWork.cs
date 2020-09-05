using LearnBySpeaking.Domain.Interfaces.Core;
using LearnBySpeaking.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace LearnBySpeaking.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LearnBySpeakingContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(LearnBySpeakingContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task StartTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException($"You did not start a Transaction by calling \"{nameof(StartTransactionAsync)}\" method.");

            await _transaction.RollbackAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException($"You did not start a Transaction by calling \"{nameof(StartTransactionAsync)}\" method. if you just need to save the data to database call \"{nameof(CommitAsync)}\" method instead of this method");

            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}