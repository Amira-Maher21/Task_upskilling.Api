using System.Linq.Expressions;
using upskilling.Domain.Interfaces.Specification;

namespace upskilling.Api.Interfaces
{
	public interface IBaseRepository<T, S> where T : class where S : class
	{
		Task<S> GetByIDAsync(int id);
		//Ahmed
		IQueryable<T> AsQueryable();
		Task<List<S>> GetAllAsync(Expression<Func<T, bool>>? predict, IBaseSpecification<T> specification = null);
		Task<S> GetOneAsync(Expression<Func<T, bool>> predict, IBaseSpecification<T> specification = null);
		Task<bool> IsExists(Expression<Func<T, bool>> predict);
		Task<S> GetByIDAsync<S>(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>>? includes = null);

		S DeleteAsync(S entity);
		Task<S> UpdateAsync(S entity, bool Save = false);
		Task<T> CreateAsync(S entity, bool Save = true);

	}


}

