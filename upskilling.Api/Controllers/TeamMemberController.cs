using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using upskilling.Api.DTOs;
using upskilling.Api.Specifications;
using upskilling.Domain.Models;
using upskilling.Repository.Data;
using upskilling.Repository.UnitOfWork;
using upskillingApi.DTOs;

namespace upskilling.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeamMemberController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		protected ResponseDto _responseDto;
		public TeamMemberController(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_responseDto = new ResponseDto();
			_mapper = mapper;
		}


		[HttpGet]
		public async Task<object> Get()
		{
			try
			{
				var Team = await _mapper.ProjectTo<TeamMemberDto>(_unitOfWork.Repository<TeamMember, TeamMemberDto>().AsQueryable()).ToListAsync();

				if (Team == null)
				{
					_responseDto.StatusCode = false;
					_responseDto.StatusCodeMassage(404);
					return _responseDto;
				}
				_responseDto.StatusCode = true;
				_responseDto.Result = Team;
			}
			catch (Exception ex)
			{
				_responseDto.StatusCode = false;
				_responseDto.StatusCodeMassage(500);
				_responseDto.Exception = new List<string>() { ex.ToString() };
			}
			return _responseDto;
		}





		[HttpGet]
		[Route("{id}")]
		public async Task<object> GetById(int id)
		{
			try
			{
					var includes = new List<Expression<Func<TeamMember, object>>>
					{
						tm => tm.Tasks // Include the Tasks navigation property
					};

				var teamMemberDto = await _unitOfWork.Repository<TeamMember, TeamMemberDto>()
					.GetByIDAsync<TeamMemberDto>(tm => tm.MemberId == id, includes);

				if (teamMemberDto == null)
				{
					_responseDto.StatusCode = false;
					_responseDto.StatusCodeMassage(404);
					return _responseDto;
				}

				_responseDto.StatusCode = true;
				_responseDto.Result = teamMemberDto;
			}
			catch (Exception ex)
			{
				_responseDto.StatusCode = false;
				_responseDto.StatusCodeMassage(500);
				_responseDto.Exception = new List<string> { ex.ToString() };
			}

			return _responseDto;
		}





		

		[HttpPost("Create")]
		public async Task<object> Create(TeamMemberDto dto)
		{
			try
			{
				if (dto == null)
				{
					_responseDto.StatusCode = false;
					_responseDto.StatusCodeMassage(404);
					return _responseDto;
				}
				_responseDto.StatusCode = true;


				await _unitOfWork.Repository<TeamMember, TeamMemberDto>().CreateAsync(dto);
				await _unitOfWork.SaveAsync();
			}
			catch (Exception ex)
			{
				_responseDto.StatusCode = false;
				_responseDto.StatusCodeMassage(500);
				_responseDto.Exception = new List<string>() { ex.ToString() };
			}
			return _responseDto;
		}


		[HttpPut]
		public async Task<object> Update(TeamMemberDto dto)
		{
			try
			{
				bool isExist = await _unitOfWork.Repository<TeamMember, TeamMemberDto>().IsExists(a => a.MemberId == dto.MemberId);
				if (!isExist)
				{
					_responseDto.StatusCode = false;
					_responseDto.StatusCodeMassage(404);
					return _responseDto;
				}
				_responseDto.StatusCode = true;


				var data = await _unitOfWork.Repository<TeamMember, TeamMemberDto>().GetByIDAsync(dto.MemberId);
				await _unitOfWork.Repository<TeamMember, TeamMemberDto>().UpdateAsync(dto);
				await _unitOfWork.SaveAsync();
			}
			catch (Exception ex)
			{
				_responseDto.StatusCode = false;
				_responseDto.StatusCodeMassage(500);
				_responseDto.Exception = new List<string> { ex.ToString() };
			}
			return _responseDto;
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<object> Delete(int id)
		{
			try
			{
				var Team = await _unitOfWork.Repository<TeamMember, TeamMemberDto>().GetByIDAsync(id);
				if (Team == null)
				{
					_responseDto.StatusCode = false;
					_responseDto.StatusCodeMassage(404);
					return _responseDto;
				}
				_responseDto.StatusCode = true;
				_unitOfWork.Repository<TeamMember, TeamMemberDto>().DeleteAsync(Team);
				await _unitOfWork.SaveAsync();
			}
			catch (Exception ex)
			{
				_responseDto.StatusCode = false;
				_responseDto.StatusCodeMassage(500);
				_responseDto.Exception = new List<string> { ex.ToString() };
			}
			return _responseDto;
		}
	}
}