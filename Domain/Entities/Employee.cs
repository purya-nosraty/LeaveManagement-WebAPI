using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Employee
{
	public Guid Id { get; set; }
	public string FullName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}
