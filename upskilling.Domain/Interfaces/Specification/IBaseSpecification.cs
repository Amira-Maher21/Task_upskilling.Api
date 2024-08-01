using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace upskilling.Domain.Interfaces.Specification
{
	public interface IBaseSpecification<T>
	{
		List<Expression<Func<T, object>>> Includes { get; }
		List<string> IncludeStrings { get; }
	}
}
