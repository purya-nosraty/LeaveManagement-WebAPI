using System;
using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Interfaces;

public interface IEmployeeRepository
{
	Task<LeaveRequest?> GetByIdAsync(Guid id);
	Task<List<LeaveRequest>> GetAllAsync();
	Task AddAsync(LeaveRequest request);
	Task<List<LeaveRequest>> GetByEmployeeIdAsync(Guid employeeId);
	Task SaveChangesAsync();
}
