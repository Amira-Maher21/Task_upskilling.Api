using System.Linq.Expressions;
using upskilling.Domain.Interfaces.Specification;
using upskilling.Domain.Models;
using upskilling.Repository.Specification;

namespace upskilling.Api.Specifications
{
	public class TeamMemberSpecification : BaseSpecification<TeamMember>
	{
		public TeamMemberSpecification() :base()
		{
 			AddInclude(M => M.Tasks);

		}
	}
}
