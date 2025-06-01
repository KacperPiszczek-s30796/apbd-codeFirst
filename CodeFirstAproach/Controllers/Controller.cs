using CodeFirstAproach.contracts.requests;
using CodeFirstAproach.Model;
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
    public async Task<IActionResult> GetTrips([FromQuery] int PatientId, CancellationToken token = default)
    {
        try
        {
            var result = service.GetPatientInfo(PatientId, token);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> isssuePrescription([FromBody] requestDTO requestDto, CancellationToken token)
    {
        try
        {
            var result = service.issuePrescription(requestDto, token);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}