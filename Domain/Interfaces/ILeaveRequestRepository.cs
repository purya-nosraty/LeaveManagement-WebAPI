using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Domain.Interfaces;

public interface ILeaveRequestRepository
{
	Task<Employee?> GetByIdAsync(Guid id);
	Task<List<LeaveRequest>> GetAllAsync();
}
