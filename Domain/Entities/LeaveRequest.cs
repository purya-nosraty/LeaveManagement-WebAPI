using Domain.Enums;
using System;

namespace Domain.Entities;

public class LeaveRequest
{
	public Guid Id { get; set; }
	public Guid EmployeeId { get; set; }
	public Employee Employee { get; set; } = null!;
	public DateTime FromDate { get; set; }
	public DateTime ToDate { get; set; }
	public string Reason { get; set; } = null!;
	public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
	public Guid? SubstituteEmployeeId { get; set; }

}
