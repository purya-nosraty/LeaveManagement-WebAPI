using System;
using System.Linq;
using Domain.Enums;
using Domain.Entities;
using Application.DTOs;
using Domain.Interfaces;
using Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Application.Services;

public class LeaveRequestService(ILeaveRequestRepository leaveRequestRepo) : ILeaveRequestService
{
	private readonly ILeaveRequestRepository _leaveRequestRepo = leaveRequestRepo;


	public async Task CreateAsync(CreateLeaveRequestDto dto)
	{
		if (dto.ToDate < dto.FromDate)
		{
			throw new ArgumentException("ToDate must be after FromDate");
		}

		if (dto.SubstituteEmployeeId.HasValue)
		{
			var overlaps = await _leaveRequestRepo
				.GetByEmployeeIdAsync(dto.SubstituteEmployeeId.Value);

			var unVerifiedSubstitute = overlaps
				.Any(l => l.Status != LeaveStatus.Rejected
					&& l.FromDate <= dto.ToDate
					&& l.ToDate >= dto.FromDate
				);

			if (unVerifiedSubstitute)
			{
				throw new InvalidOperationException("Substitute is on leave during this period");
			}
		}

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

		await _leaveRequestRepo.AddAsync(entity);
		await _leaveRequestRepo.SaveChangesAsync();
	}

	public async Task<IEnumerable<LeaveRequestDto>> GetByEmployeeAsync(Guid employeeId)
	{
		var list = await _leaveRequestRepo.GetByEmployeeIdAsync(employeeId);

		return list.Select(t =>
			new LeaveRequestDto(
				t.Id,
				t.EmployeeId,
				t.FromDate,
				t.ToDate,
				t.Reason,
				t.Status.ToString(),
				t.SubstituteEmployeeId
			)
		);
	}

	public async Task<IEnumerable<LeaveRequestDto>> GetAllAsync()
	{
		var list = await _leaveRequestRepo.GetAllAsync();

		return list.Select(t =>
			new LeaveRequestDto(
				t.Id,
				t.EmployeeId,
				t.FromDate,
				t.ToDate,
				t.Reason,
				t.Status.ToString(),
				t.SubstituteEmployeeId
			)
		);
	}

	public async Task ApproveAsync(Guid requestId)
	{
		var leave = await _leaveRequestRepo.GetByIdAsync(requestId)
			?? throw new KeyNotFoundException("Leave request not found");

		leave.Status = LeaveStatus.Approved;

		await
			_leaveRequestRepo.SaveChangesAsync();
	}

	public async Task RejectAsync(Guid requestId)
	{
		var leave = await _leaveRequestRepo.GetByIdAsync(requestId)
			?? throw new KeyNotFoundException("Leave request not found");

		leave.Status = LeaveStatus.Rejected;

		await
			_leaveRequestRepo.SaveChangesAsync();
	}
}
