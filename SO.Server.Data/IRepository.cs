using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace SO.Server.Data {
	public interface IRepository<T> where T : class
	{
		T Search(params object[] keyValues);

		T Single(Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool disableTracking = true);

		//IPaginate<T> GetList(Expression<Func<T, bool>> predicate = null,
		//	Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
		//	Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
		//	int index = 0,
		//	int size = 20,
		//	bool disableTracking = true);

		//IPaginate<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector,
		//	Expression<Func<T, bool>> predicate = null,
		//	Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
		//	Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
		//	int index = 0,
		//	int size = 20,
		//	bool disableTracking = true) where TResult : class;

		void Add(T entity);
		void Add(IEnumerable<T> entities);


		void Delete(T entity);
		void Delete(IEnumerable<T> entities);


		void Update(T entity);
		void Update(IEnumerable<T> entities);
	}
}