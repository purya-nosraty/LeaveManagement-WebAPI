using System;
using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Interfaces;

public interface ILeaveRequestRepository
{
	Task SaveChangesAsync();

	Task AddAsync(LeaveRequest leaveRequest);

	Task<LeaveRequest?> GetByIdAsync(Guid id);

	Task<List<LeaveRequest>> GetAllAsync();

	Task<List<LeaveRequest>> GetByEmployeeIdAsync(Guid id);
}
