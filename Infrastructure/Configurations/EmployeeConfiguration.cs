using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class EmployeeConfiguration() : IEntityTypeConfiguration<Employee>
{
	public void Configure(EntityTypeBuilder<Employee> builder)
	{
		#region Id
		builder
			.HasKey(current => current.Id)
			.IsClustered(clustered: false)
		;
		#endregion /Id

		//***********************************************

		#region FullName
		builder
			.Property(current => current.FullName)
			.HasColumnName(name: nameof(Domain.Shared.Resources.DataDictionary.FullName))
			.IsRequired(required: true)
			.IsUnicode(unicode: true)
			.IsFixedLength(fixedLength: false)
			.HasMaxLength(maxLength: Domain.Shared.Utility.Const.FullNameMaxLength)
		;

		builder
			.HasIndex(current => current.FullName)
			.IsUnique(unique: true)
		;
		#endregion /FullName

		//***********************************************

		#region Email
		builder
			.Property(current => current.Email)
			.IsRequired(required: true)
		;
		#endregion /Email

		//***********************************************
	}
}
