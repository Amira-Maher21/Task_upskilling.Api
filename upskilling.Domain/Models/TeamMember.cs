using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace upskilling.Domain.Models
{
	public class TeamMember
	{
 		public int MemberId { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		public virtual ICollection<Tasks> Tasks { get; set; }= new List<Tasks>();	

	}
}
