using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum LeaveStatus
{
	[Display(Description = nameof(Shared.Resources.DataDictionary.Pending))]
	Pending = 0,

	[Display(Description = nameof(Shared.Resources.DataDictionary.Approved))]
	Approved = 1,

	[Display(Description = nameof(Shared.Resources.DataDictionary.Rejected))]
	Rejected = 2,
}
