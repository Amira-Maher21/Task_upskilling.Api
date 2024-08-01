using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upskilling.Api.Interfaces;

namespace upskilling.Repository.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IBaseRepository<TEntity, SDto> Repository<TEntity, SDto>() where TEntity : class where SDto : class;
 		Task<int> SaveAsync();
	}
}
