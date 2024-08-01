using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using upskilling.Api.DTOs;
using upskilling.Domain.Interfaces.Specification;
using upskilling.Domain.Models;
using upskilling.Repository.UnitOfWork;
using upskillingApi.DTOs;
using static Azure.Core.HttpHeader;

namespace upskilling.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TasksController : ControllerBase
	{
		private readonly IUnitOfWork _unitOfWork;
		protected ResponseDto _responseDto;
		public TasksController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_responseDto = new ResponseDto();
		}

		[HttpGet]
		public async Task<object> Get()
		{
			try
			{
				var TeamM = await _unitOfWork.Repository<Tasks, TasksDto>().GetAllAsync(null);
 
 					 
				 
				if (TeamM == null)
				{
					_responseDto.StatusCode = false;
					_responseDto.StatusCodeMassage(404);
					return _responseDto;
				}
				_responseDto.StatusCode = true;
				_responseDto.Result = TeamM;
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
				var Data = await _unitOfWork.Repository<Tasks, TasksDto>().GetOneAsync(a => a.TaskId == id);
				if (Data == null)
				{
					_responseDto.StatusCode = false;
					_responseDto.StatusCodeMassage(404);
					return _responseDto;
				}
				_responseDto.StatusCode = true;
				_responseDto.Result = Data;
			}
			catch (Exception ex)
			{
				_responseDto.StatusCode = false;
				_responseDto.StatusCodeMassage(500);
				_responseDto.Exception = new List<string>() { ex.ToString() };

			}
			return _responseDto;
		}

		[HttpPost("Create")]
		public async Task<object> Create(TasksDto dto)
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

  
				await _unitOfWork.Repository<Tasks, TasksDto>().CreateAsync(dto);
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
		public async Task<object> Update(TasksDto dto)
		{
			try
			{
				bool isExist = await _unitOfWork.Repository<Tasks, TasksDto>().IsExists(a => a.TaskId == dto.TaskId);
				if (!isExist)
				{
					_responseDto.StatusCode = false;
					_responseDto.StatusCodeMassage(404);
					return _responseDto;
				}
				_responseDto.StatusCode = true;

  
				var data = await _unitOfWork.Repository<Tasks, TasksDto>().GetByIDAsync(dto.TaskId);
				await _unitOfWork.Repository<Tasks, TasksDto>().UpdateAsync(dto);
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
				var Data = await _unitOfWork.Repository<Tasks, TasksDto>().GetByIDAsync(id);
				if (Data == null)
				{
					_responseDto.StatusCode = false;
					_responseDto.StatusCodeMassage(404);
					return _responseDto;
				}
				_responseDto.StatusCode = true;
				_unitOfWork.Repository<Tasks, TasksDto>().DeleteAsync(Data);
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
