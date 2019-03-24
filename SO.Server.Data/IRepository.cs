using Microsoft.EntityFrameworkCore.Query;
using SO.Server.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SO.Server.Data
{
    public interface IRepository<T> where T : IEntity
    {
        T Search(params object[] keyValues);

        T FirstOrDefault(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true);

        void Add(T entity);

        void Add(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(IEnumerable<T> entities);

        void Update(T entity);

        void Update(IEnumerable<T> entities);
    }
}