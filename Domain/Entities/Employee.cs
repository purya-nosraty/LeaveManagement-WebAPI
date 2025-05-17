using System;
using Domain.Shared;
using System.Collections.Generic;
using Domain.Shared.Resources.Messages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Employee
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
	/// نام و نام خانوادگی
	/// </summary>
	[Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.Required))]
	[StringLength
		(maximumLength: Utility.Const.FullNameMaxLength,
		MinimumLength = Utility.Const.FullNameMinLength,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.StringLength))]
	[Display(Name = nameof(Shared.Resources.DataDictionary.FullName))]
	public string FullName { get; set; } = null!;


	/// <summary>
	/// ایمیل
	/// </summary>
	[Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.Required))]
	[RegularExpression
		(pattern: Utility.Regex.Email,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.EmailAddress),
		MatchTimeoutInMilliseconds = 0)]
	[Display(Name = nameof(Shared.Resources.DataDictionary.EmailAddress))]
	public string Email { get; set; } = null!;


	/// <summary>
	/// درخواست مرخصی
	/// </summary>
	public ICollection<LeaveRequest> LeaveRequests { get; } = [];
}
