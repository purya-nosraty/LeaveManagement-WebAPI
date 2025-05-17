using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
	#region Constructor
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
		Database.EnsureDeleted();
		Database.EnsureCreated();
	}
	#endregion /Constructor



	#region Properties
	public DbSet<Employee> Employees { get; set; } = null!;
	public DbSet<LeaveRequest> LeaveRequests { get; set; } = null!;
	#endregion /Properties



	#region Methods
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder
			.ApplyConfigurationsFromAssembly
				(assembly: typeof(AppDbContext).Assembly);

		modelBuilder
			.Entity<Employee>()
			.HasKey(e => e.Id);

		modelBuilder
			.Entity<LeaveRequest>(entity =>
			{
				entity.HasKey(l => l.Id);
				entity
				  .HasOne(l => l.Employee)
				  .WithMany(e => e.LeaveRequests)
				  .HasForeignKey(l => l.EmployeeId);
			});
	}
	#endregion /Methods
}
