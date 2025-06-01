using CodeFirstAproach.Services.abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstAproach.Controllers;
[ApiController]
[Microsoft.AspNetCore.Components.Route("api")]
public class Controller: ControllerBase
{
    private IService service;

    public Controller(IService service)
    {
        this.service = service;
    }
    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] int PatientId = 1, CancellationToken token = default)
    {
        return Ok();
    }
}