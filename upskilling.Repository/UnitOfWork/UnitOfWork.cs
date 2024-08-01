using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upskilling.Api.Interfaces;
using upskilling.Repository.Data;
using upskilling.Repository.Repositories;

namespace upskilling.Repository.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly upskillingDbContext _context;
		private Hashtable _repositories;
 		private readonly IMapper _mapper;

 
		public UnitOfWork(upskillingDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
			_repositories = new Hashtable();

		}

		public IBaseRepository<TEntity, SDto> Repository<TEntity, SDto>() where TEntity : class where SDto : class
		{
			if (_repositories == null)
				_repositories = new Hashtable();
			var type = typeof(TEntity).Name;

			if (!_repositories.ContainsKey(type))
			{
				var repository = new BaseRepository<TEntity, SDto>(_context, _mapper);
				_repositories.Add(type, repository);
			}

			return (IBaseRepository<TEntity, SDto>)_repositories[type];
		}

 
		public async Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}

		 
	}
}
