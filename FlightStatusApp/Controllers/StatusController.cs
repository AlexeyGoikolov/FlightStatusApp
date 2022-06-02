using FlightStatusApp.Models;
using FlightStatusApp.Repositories;
using FlightStatusApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightStatusApp.Controllers;


[ApiController]
[Route("api/[controller]")]
public class StatusController : Controller
{
    private IRepository _db;
    private AuthorizationService _authService;

    public StatusController(IRepository db, AuthorizationService authService)
    {
        _db = db;
        _authService = authService;
    }

    [HttpPost("/token")]
    public async Task<ActionResult> GetToken(string username, string password)
    {
         var response = await _authService.GetToken(username, password);
         return Json(response);
    }
    [Authorize]
    [HttpGet]
    [Route("/getstatus")]
    public async Task<ActionResult<IEnumerable<Flight>>> Get()
    {
        var statuses = await _db.GetAllStatuses();
        return new ObjectResult(statuses);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Flight>>> Get([FromQuery] string origin, string destination)
    {
        var statuses = await _db.GetFilteredStatuses(origin, destination);
        if (statuses is null)
            return NotFound();
        return new ObjectResult(statuses);
    }
    
    [Authorize(Roles = "Moderator")]
    [HttpPost]
    public async Task<ActionResult<Flight>> Post(Flight flight)
    {
        if (flight is null)
            return BadRequest();
        var addedStatus = await _db.Create(flight);
        return Ok(addedStatus);
    }
    [Authorize(Roles = "Moderator")]
    [HttpPut]
    public async Task<ActionResult<Flight>> Put(Flight flight)
    {
        if (flight is null)
            return BadRequest();
        if (_db.GetStatus(flight.Id) is null)
            return NotFound();

        var updateStatus = await _db.Update(flight);
        return Ok(updateStatus);
    }


    
    
}