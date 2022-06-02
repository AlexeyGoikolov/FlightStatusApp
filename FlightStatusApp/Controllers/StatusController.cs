using FlightStatusApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightStatusApp.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StatusController : Controller
{
    [HttpPost("/token")]
    public IActionResult GetToken(string username, string password)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public Task<IActionResult> Get()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string origin, string destination)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<Flight>> Post(Flight flight)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public async Task<ActionResult<Flight>> Put(Flight country)
    {
        throw new NotImplementedException();
    }


    
    
}