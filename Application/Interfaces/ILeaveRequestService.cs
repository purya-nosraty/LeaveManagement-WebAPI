using System;
using Application.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Application.Interfaces;

public interface ILeaveRequestService
{

	Task CreateAsync(CreateLeaveRequestDto dto);

	Task<IEnumerable<LeaveRequestDto>> GetByEmployeeAsync(Guid employeeId);

	Task<IEnumerable<LeaveRequestDto>> GetAllAsync();

	Task ApproveAsync(Guid requestId);

	Task RejectAsync(Guid requestId);
}
