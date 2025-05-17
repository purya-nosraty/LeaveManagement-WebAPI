using System;
using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Domain.Interfaces;

public interface IEmployeeRepository
{
	Task SaveChangesAsync();

	Task AddAsync(Employee employee);

	Task<List<Employee>> GetAllAsync();

	Task<Employee?> GetByIdAsync(Guid id);
}
