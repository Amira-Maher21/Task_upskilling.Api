using AutoMapper;
using ClosedXML.Excel;
using upskilling.Api.DTOs;
using upskilling.Domain.Models;

namespace upskilling.Api.Infrastructure
{
	public class MappingProfile : Profile
	{
		public MappingProfile() 
		{

			CreateMap<Tasks,TasksDto>()
				.ReverseMap();

			CreateMap<TeamMember, TeamMemberDto>()
				.ReverseMap();


			CreateMap<TeamMember, TeamMemberDto>()
		.ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks))
		// Add other mappings here
		;

			CreateMap<Task, TasksDto>();
		}


	}
}
 
 