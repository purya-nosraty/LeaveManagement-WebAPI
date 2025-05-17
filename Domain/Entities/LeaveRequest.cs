using System;
using Domain.Enums;
using Domain.Shared;
using Domain.Shared.Resources.Messages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class LeaveRequest
{
	/// <summary>
	/// شناسه
	/// </summary>
	[Key]
	[Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.Required))]
	[DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.None)]
	public Guid Id { get; set; }


	/// <summary>
	/// شناسه کارمندی
	/// </summary>
	[Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.Required))]
	[DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.None)]
	public Guid EmployeeId { get; set; }
	public virtual Employee? Employee { get; set; } = null!;


	/// <summary>
	/// از تاریخِ
	/// </summary>
	[Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.Required))]
	[DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.None)]
	public DateTime FromDate { get; set; }


	/// <summary>
	/// تا تاریخِ
	/// </summary>
	[Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.Required))]
	[DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.None)]
	public DateTime ToDate { get; set; }


	/// <summary>
	/// علت
	/// </summary>
	[Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.Required))]
	[StringLength
		(maximumLength: Utility.Const.ReasonMaxLength,
		MinimumLength = Utility.Const.ReasonMinLength,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.StringLength))]
	public string Reason { get; set; } = null!;


	/// <summary>
	/// وضعیت
	/// </summary>
	[Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.Required))]
	public LeaveStatus Status { get; set; } = LeaveStatus.Pending;


	/// <summary>
	/// کارمند جایگزین
	/// </summary>
	public Guid? SubstituteEmployeeId { get; set; }
}
