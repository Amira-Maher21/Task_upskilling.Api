using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using upskilling.Domain.Interfaces.Specification;

namespace upskilling.Repository.Specification
{
	public abstract class BaseSpecification<T> : IBaseSpecification<T>
	{
		 
		protected BaseSpecification()
		{

		}

		public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
		public List<string> IncludeStrings { get; } = new List<string>();

		protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
		{
			Includes.Add(includeExpression);
		}

	}
}
	