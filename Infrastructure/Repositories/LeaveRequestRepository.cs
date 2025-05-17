using System;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LeaveRequestRepository(AppDbContext appDbContext) : ILeaveRequestRepository
{
	private readonly AppDbContext _appDbContext = appDbContext;


	#region Methods
	public async Task SaveChangesAsync()
	{
		await _appDbContext.SaveChangesAsync();
	}

	public async Task AddAsync(LeaveRequest leaveRequest)
	{
		await _appDbContext.LeaveRequests.AddAsync(leaveRequest);
	}

	public async Task<LeaveRequest?> GetByIdAsync(Guid id)
	{
		return await
			_appDbContext.LeaveRequests
				.Include(l => l.Employee)
				.FirstOrDefaultAsync(l => l.Id == id);
	}

	public async Task<List<LeaveRequest>> GetAllAsync()
	{
		return await
			_appDbContext.LeaveRequests
				.Include(l => l.Employee)
				.ToListAsync();
	}

	public async Task<List<LeaveRequest>> GetByEmployeeIdAsync(Guid id)
	{
		return await
			_appDbContext.LeaveRequests
				.Include(l => l.Employee)
				.Where(l => l.EmployeeId == id)
				.ToListAsync();
	}
	#endregion /Methods
}
