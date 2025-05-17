using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum LeaveStatus
{
	[Display(Description = "در انتظار تایید")]
	Pending = 0,

	[Display(Description = "تایید شده")]
	Approved = 1,

	[Display(Description = "مردود")]
	Rejected = 2,
}
