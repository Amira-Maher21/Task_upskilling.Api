using Microsoft.Build.Framework;
using upskilling.Domain.Models;

namespace upskilling.Api.DTOs
{
	public class TasksDto
	{
		public int TaskId { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		public string Status { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

	}
}
