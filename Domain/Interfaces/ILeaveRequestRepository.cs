using System;
using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Interfaces;

public interface ILeaveRequestRepository
{
	Task<LeaveRequest?> GetByIdAsync(Guid id);

	Task<List<LeaveRequest>> GetAllAsync();
}
