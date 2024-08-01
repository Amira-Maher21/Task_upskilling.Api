using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using upskilling.Api.Interfaces;
using upskilling.Domain.Interfaces.Specification;
using upskilling.Repository.Data;

namespace upskilling.Repository.Repositories
{

	public class BaseRepository<T, S> : IBaseRepository<T, S> where T : class where S : class
	{
		protected readonly upskillingDbContext _dbContext;
		private readonly IMapper _mapper;


		public BaseRepository(upskillingDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<T> CreateAsync(S dto, bool Save = true)
		{
			T entity = _mapper.Map<T>(dto);
			await _dbContext.Set<T>().AddAsync(entity);
			if (Save)
			{
				await _dbContext.SaveChangesAsync();
			}
			return entity;
		}

		public S DeleteAsync(S entity)
		{
			_dbContext.Set<T>().Remove(_mapper.Map<T>(entity));
			return entity;
		}
		//Ahmed
		public IQueryable<T> AsQueryable()=> _dbContext.Set<T>().AsQueryable();

		public List<S> DeleteAllAsync(List<S> entities)
		{
			_dbContext.Set<T>().RemoveRange(_mapper.Map<List<T>>(entities));
			return entities;
		}

		public async Task<List<S>> GetAllAsync(Expression<Func<T, bool>>? predict, IBaseSpecification<T> specification = null)
		{
			IQueryable<T> objQuery;
			if (specification != null)
			{
				objQuery = ApplySpecification(specification);
			}
			else
				objQuery = _dbContext.Set<T>();

			if (predict != null)
			{
				objQuery = objQuery.Where(predict);
			}
			var data = await objQuery.ToListAsync();
			var dtos = _mapper.Map<List<S>>(data);
			return dtos;
		}

		private IQueryable<T> ApplySpecification(IBaseSpecification<T> spec)
		{
			return _dbContext.Set<T>().AsQueryable();

		}

		 


		public async Task<S> GetByIDAsync(int id)
		{
			var dto = await _dbContext.Set<T>().FindAsync(id);
			return _mapper.Map<S>(dto);
		}

		public async Task<S> UpdateAsync(S dto, bool Save = false)
		{
			T entity = _mapper.Map<T>(dto);

			_dbContext.Set<T>().Update(entity);
			if (Save)
			{
				await _dbContext.SaveChangesAsync();
			}
			dto = _mapper.Map<S>(entity);
			return dto;
		}



		public async Task<S> GetOneAsync(Expression<Func<T, bool>>? predict, IBaseSpecification<T> specification = null)
		{
 
			IQueryable<T> objQuery = ApplySpecification(specification);
			
			if (predict != null)
			{
				objQuery = objQuery.Where(predict);
			}
			var dto = await objQuery.FirstOrDefaultAsync();
			return _mapper.Map<S>(dto);
		}

		public async Task<bool> IsExists(Expression<Func<T, bool>> predict)
		{
			return await _dbContext.Set<T>().AnyAsync(predict);
		}


		public async Task<S> GetByIDAsync<S>(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>>? includes = null)
		{
			IQueryable<T> query = _dbContext.Set<T>().AsQueryable();

			// Apply includes if provided
			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			// Apply the predicate
			query = query.Where(predicate);

			// Fetch the first matching entity
			var entity = await query.FirstOrDefaultAsync();

			// Map the entity to the DTO
			return _mapper.Map<S>(entity);
		}

	}

}
