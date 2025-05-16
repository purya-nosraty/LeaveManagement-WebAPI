using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}
	public DbSet<Employee> Employees { get; set; } = null!;
	public DbSet<LeaveRequest> LeaveRequests { get; set; } = null!;
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Employee>().HasKey(e => e.Id);

		modelBuilder.Entity<LeaveRequest>(entity =>
		{
			entity.HasKey(l => l.Id);
			entity
			  .HasOne(l => l.Employee)
			  .WithMany(e => e.LeaveRequests)
			  .HasForeignKey(l => l.EmployeeId);
		});
	}
}
