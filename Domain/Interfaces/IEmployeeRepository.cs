using System;
using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Interfaces;

public interface IEmployeeRepository
{
	Task SaveChangesAsync();

	Task AddAsync(LeaveRequest request);

	Task<List<LeaveRequest>> GetAllAsync();

	Task<LeaveRequest?> GetByIdAsync(Guid id);

	Task<List<LeaveRequest>> GetByEmployeeIdAsync(Guid employeeId);
}
