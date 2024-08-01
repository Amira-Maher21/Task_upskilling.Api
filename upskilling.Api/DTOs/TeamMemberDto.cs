using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System.ComponentModel.DataAnnotations;
 using upskilling.Domain.Models;

namespace upskilling.Api.DTOs
{
	public class TeamMemberDto
	{
		public int MemberId { get; set; }
		[Required]

		public string Name { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		public List<TasksDto> Tasks { get; set; }

		int[] arr;


	}

}
