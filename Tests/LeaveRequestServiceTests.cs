using Moq;
using System;
using Domain.Enums;
using Domain.Entities;
using Application.DTOs;
using Domain.Interfaces;
using Application.Services;
using System.Threading.Tasks;

namespace Tests;

public class LeaveRequestServiceTests
{
    private readonly LeaveRequestService _service;
    private readonly Mock<IEmployeeRepository> _empRepoMock;
    private readonly Mock<ILeaveRequestRepository> _leaveRepoMock;

    public LeaveRequestServiceTests()
    {
        _empRepoMock = new Mock<IEmployeeRepository>();
        _leaveRepoMock = new Mock<ILeaveRequestRepository>();

        _empRepoMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => new Employee { Id = id });

        _service = new LeaveRequestService(_leaveRepoMock.Object);
    }

    [Fact]
    public async Task CreateAsync_InvalidDateRange_ThrowsArgumentException()
    {
        var dto = new CreateLeaveRequestDto(
            EmployeeId: Guid.NewGuid(),
            FromDate: DateTime.Today.AddDays(5),
            ToDate: DateTime.Today,
            Reason: "Test",
            SubstituteEmployeeId: null
        );

        await Assert
            .ThrowsAsync<ArgumentException>(() => _service.CreateAsync(dto));
    }

    [Fact]
    public async Task CreateAsync_SubstituteOnLeave_ThrowsInvalidOperationException()
    {
        var empId = Guid.NewGuid();
        var subId = Guid.NewGuid();

        _leaveRepoMock
            .Setup(r => r.GetByEmployeeIdAsync(subId))
            .ReturnsAsync([
                    new LeaveRequest {
                        Id = Guid.NewGuid(),
                        EmployeeId = subId,
                        FromDate = DateTime.Today,
                        ToDate = DateTime.Today.AddDays(2),
                        Status = LeaveStatus.Pending,
                        Reason = "Overlap",
                        SubstituteEmployeeId = null
                    }
            ]);

        var dto = new CreateLeaveRequestDto(
            EmployeeId: empId,
            FromDate: DateTime.Today,
            ToDate: DateTime.Today.AddDays(1),
            Reason: "Test",
            SubstituteEmployeeId: subId
        );

        await Assert
            .ThrowsAsync<InvalidOperationException>(() => _service.CreateAsync(dto));
    }

    [Fact]
    public async Task CreateAsync_ValidRequest_CallsRepositoryAddAndSave()
    {
        var empId = Guid.NewGuid();

        var dto = new CreateLeaveRequestDto(
            EmployeeId: empId,
            FromDate: DateTime.Today,
            ToDate: DateTime.Today.AddDays(1),
            Reason: "OK",
            SubstituteEmployeeId: null
        );

        _leaveRepoMock
            .Setup(r => r.GetByEmployeeIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync([]);

        _leaveRepoMock
            .Setup(r => r.AddAsync(It.IsAny<LeaveRequest>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        _leaveRepoMock
            .Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask)
            .Verifiable();

        var resultId = await _service.CreateAsync(dto);

        _leaveRepoMock
            .Verify(r => r.AddAsync(It.Is<LeaveRequest>(
                l => l.EmployeeId == empId && l.Reason == "OK")
                ), Times.Once
            );

        _leaveRepoMock
            .Verify(r => r.SaveChangesAsync(), Times.Once);

        Assert.NotEqual(Guid.Empty, resultId);
    }
}