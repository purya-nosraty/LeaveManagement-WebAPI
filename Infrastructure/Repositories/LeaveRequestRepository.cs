using System;
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
	#endregion /Methods
}
