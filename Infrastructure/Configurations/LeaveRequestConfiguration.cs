using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class LeaveRequestConfiguration() : IEntityTypeConfiguration<LeaveRequest>
{
	public void Configure(EntityTypeBuilder<LeaveRequest> builder)
	{
		#region Id
		builder
			.HasKey(current => current.Id)
			.IsClustered(clustered: false)
		;
		#endregion /Id

		//***********************************************

		#region EmployeeId
		builder
			.Property(current => current.EmployeeId)
			.IsRequired(required: true)
		;
		#endregion /EmployeeId

		//***********************************************

		#region FromDate
		builder
			.Property(current => current.FromDate)
			.IsRequired(required: true)
		;
		#endregion /FromDate

		//***********************************************

		#region ToDate
		builder
			.Property(current => current.ToDate)
			.IsRequired(required: true)
		;
		#endregion /ToDate

		//***********************************************

		#region Reason
		builder
			.Property(current => current.Reason)
			.IsRequired(required: true)
			.IsUnicode(unicode: true)
			.IsFixedLength(fixedLength: false)
			.HasMaxLength(maxLength: Domain.Shared.Utility.Const.ReasonMaxLength)
		;
		#endregion /Reason

		//***********************************************

		#region Status
		builder
			.Property(current => current.Status)
			.HasColumnName(name: nameof(Domain.Shared.Resources.DataDictionary.Status))
		;
		#endregion /Status

		//***********************************************
	}
}
