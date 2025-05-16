using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Infrastructure.Repositories;

public class LeaveRequestRepository : ILeaveRequestRepository
{
	private readonly AppDbContext _ctx;
	public LeaveRequestRepository(AppDbContext ctx) => _ctx = ctx;

	public async Task AddAsync(LeaveRequest request) => await _ctx.LeaveRequests.AddAsync(request);
	public async Task<List<LeaveRequest>> GetAllAsync() => await _ctx.LeaveRequests.Include(l => l.Employee).ToListAsync();
	public async Task<LeaveRequest?> GetByIdAsync(Guid id) => await _ctx.LeaveRequests.Include(l => l.Employee).FirstOrDefaultAsync(l => l.Id == id);
	public async Task<List<LeaveRequest>> GetByEmployeeIdAsync(Guid employeeId) => await _ctx.LeaveRequests.Where(l => l.EmployeeId == employeeId).ToListAsync();
	public async Task SaveChangesAsync() => await _ctx.SaveChangesAsync();

	Task<Employee?> ILeaveRequestRepository.GetByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}
}
