using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upskilling.Domain.Models
{
	public class Tasks
	{
		 
		public int TaskId { get; set; }

		[Required]
 		public string Name { get; set; }

		[Required]
 		public string Description { get; set; }

 		public string Status { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		[Required]
		public int MemberId { get; set; }
		public TeamMember TeamMember { get; set; }
		

	}
}
