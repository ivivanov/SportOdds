using Microsoft.EntityFrameworkCore;
using SO.Data.Entities;
using System;

namespace SO.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

        int SaveChanges();

        void BulkSaveChanges();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
    }
}
