using System;

namespace Application.DTOs;

public record LeaveRequestDto(
	Guid Id,
	Guid EmployeeId,
	DateTime FromDate,
	DateTime ToDate,
	string Reason,
	string Status,
	Guid? SubstituteEmployeeId
);