using Microsoft.EntityFrameworkCore;
using SO.Data.Entities;
using System;
using System.Collections.Generic;

namespace SO.Data.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, IDisposable
    {
        private readonly TContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public void BulkSaveChanges()
        {
            _context.BulkSaveChanges();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type))
                _repositories[type] = new Repository<TEntity>(_context);

            return (IRepository<TEntity>)_repositories[type];
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
