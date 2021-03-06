﻿using LearnBySpeaking.Domain.Interfaces.Core;
using LearnBySpeaking.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBySpeaking.Infra.Data.Repository.Core
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly LearnBySpeakingContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(LearnBySpeakingContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public async Task AddAsync(TEntity obj)
        {
            await DbSet.AddAsync(obj);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.Factory.StartNew(() => DbSet);
        }

        public async Task RemoveAsync(int id)
        {
            DbSet.Remove(await DbSet.FindAsync(id));
        }

        public async Task UpdateAsync(TEntity obj)
        {
            await Task.Factory.StartNew(() => { DbSet.Update(obj); });
        }

        public async Task<int> CommitAsync()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}