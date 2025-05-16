using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Application.Services;

public class LeaveRequestService : ILeaveRequestService
{
	private readonly ILeaveRequestRepository _leaveRepo;
	private readonly IEmployeeRepository _empRepo;

	public LeaveRequestService(ILeaveRequestRepository leaveRepo, IEmployeeRepository empRepo)
	{
		_leaveRepo = leaveRepo;
		_empRepo = empRepo;
	}

	public async Task<Guid> CreateAsync(CreateLeaveRequestDto dto)
	{
		if (dto.ToDate < dto.FromDate)
			throw new ArgumentException("ToDate must be after FromDate");

		//if (dto.SubstituteEmployeeId.HasValue)
		//{
		//	var sub = await _empRepo.GetByIdAsync(dto.SubstituteEmployeeId.Value)
		//		?? throw new KeyNotFoundException("Substitute employee not found");
		//	var overlaps = await _leaveRepo.GetByEmployeeIdAsync(dto.SubstituteEmployeeId.Value);
		//	if (overlaps.Any(l => l.Status != LeaveStatus.Rejected
		//						  && l.FromDate <= dto.ToDate
		//						  && l.ToDate >= dto.FromDate))
		//		throw new InvalidOperationException("Substitute is on leave during this period");
		//}

		var entity = new LeaveRequest
		{
			Id = Guid.NewGuid(),
			EmployeeId = dto.EmployeeId,
			FromDate = dto.FromDate,
			ToDate = dto.ToDate,
			Reason = dto.Reason,
			Status = LeaveStatus.Pending,
			SubstituteEmployeeId = dto.SubstituteEmployeeId
		};

		//await _leaveRepo.AddAsync(entity);
		//await _leaveRepo.SaveChangesAsync();

		return entity.Id;
	}

	//public async Task<IEnumerable<LeaveRequestDto>> GetByEmployeeAsync(Guid employeeId)
	//{
	//	var list = await _leaveRepo.GetByEmployeeIdAsync(employeeId);
	//	return list.Select(l => new LeaveRequestDto(
	//		l.Id,
	//		l.EmployeeId,
	//		l.FromDate,
	//		l.ToDate,
	//		l.Reason,
	//		l.Status.ToString(),
	//		l.SubstituteEmployeeId));
	//}

	public async Task<IEnumerable<LeaveRequestDto>> GetAllAsync()
	{
		var list = await _leaveRepo.GetAllAsync();
		return list.Select(l => new LeaveRequestDto(
			l.Id,
			l.EmployeeId,
			l.FromDate,
			l.ToDate,
			l.Reason,
			l.Status.ToString(),
			l.SubstituteEmployeeId));
	}

	public async Task ApproveAsync(Guid requestId)
	{
		var leave = await _leaveRepo.GetByIdAsync(requestId)
			?? throw new KeyNotFoundException("Leave request not found");
		//leave.Status = LeaveStatus.Approved;
		//await _leaveRepo.SaveChangesAsync();
	}

	public async Task RejectAsync(Guid requestId)
	{
		var leave = await _leaveRepo.GetByIdAsync(requestId)
			?? throw new KeyNotFoundException("Leave request not found");
		//leave.Status = LeaveStatus.Rejected;
		//await _leaveRepo.SaveChangesAsync();
	}

	Task ILeaveRequestService.CreateAsync(CreateLeaveRequestDto dto)
	{
		return CreateAsync(dto);
	}

	Task<IEnumerable<LeaveRequestDto>> ILeaveRequestService.GetByEmployeeAsync(Guid employeeId)
	{
		throw new NotImplementedException();
	}
}
