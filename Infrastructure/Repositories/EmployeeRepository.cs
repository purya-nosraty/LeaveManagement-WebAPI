using System;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
	private readonly AppDbContext _context;

	public EmployeeRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Employee?> GetByIdAsync(Guid id)
	{
		return await _context.Employees
			.Include(e => e.LeaveRequests)
			.FirstOrDefaultAsync(e => e.Id == id);
	}

	public async Task<List<Employee>> GetAllAsync()
	{
		return await _context.Employees
			.Include(e => e.LeaveRequests)
			.ToListAsync();
	}

	public async Task AddAsync(Employee employee)
	{
		await _context.Employees.AddAsync(employee);
	}

	//public async Task UpdateAsync(Employee employee)
	//{
	//	_context.Employees.Update(employee);
	//	await Task.CompletedTask;
	//}

	//public async Task DeleteAsync(Guid id)
	//{
	//	var entity = await _context.Employees.FindAsync(id);
	//	if (entity != null)
	//	{
	//		_context.Employees.Remove(entity);
	//	}
	//}
	public async Task<Employee?> GetByEmployeeIdAsync(Guid employeeId)
	{
		return await _context.Employees
			.Include(e => e.LeaveRequests)
			.FirstOrDefaultAsync(e => e.Id == employeeId);
	}
	public async Task SaveChangesAsync()
	{
		await _context.SaveChangesAsync();
	}

	Task<LeaveRequest?> IEmployeeRepository.GetByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	Task<List<LeaveRequest>> IEmployeeRepository.GetAllAsync()
	{
		throw new NotImplementedException();
	}

	Task IEmployeeRepository.AddAsync(LeaveRequest request)
	{
		throw new NotImplementedException();
	}

	Task<List<LeaveRequest>> IEmployeeRepository.GetByEmployeeIdAsync(Guid employeeId)
	{
		throw new NotImplementedException();
	}
}
