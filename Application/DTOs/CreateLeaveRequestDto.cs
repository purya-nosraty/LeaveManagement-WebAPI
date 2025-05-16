using System;

namespace Application.DTOs;

public record CreateLeaveRequestDto(
	Guid EmployeeId,
	DateTime FromDate,
	DateTime ToDate,
	string Reason,
	Guid? SubstituteEmployeeId
);
