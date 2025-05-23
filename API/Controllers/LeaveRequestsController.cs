using Domain.Interfaces;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveRequestsController(
    IEmployeeRepository employeeRepository,
    ILeaveRequestService leaveRequestService,
    ILeaveRequestRepository leaveRequestRepository
    ) : ControllerBase
{
    private readonly ILeaveRequestService
        _leaveRequestService = leaveRequestService;

    private readonly IEmployeeRepository
        _employeeRepository = employeeRepository;

    private readonly ILeaveRequestRepository
        _leaveRequestRepository = leaveRequestRepository;

    [HttpGet]
    public IActionResult GetList()
    {
        var x = _leaveRequestService.GetAllAsync();
        var a = _leaveRequestRepository.GetAllAsync();
        var b = _employeeRepository.GetAllAsync();

        return Ok(x);
    }
}