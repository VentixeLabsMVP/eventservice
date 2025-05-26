using EventApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace EventApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(EventServices eventService) : ControllerBase
{
    private readonly EventServices _eventService = eventService;


    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var result = await _eventService.GetAllAsync();
        if (result == null) 
            return NotFound();
        return Ok(result); // ok(return) method in controllerBase. is a part of Iactionresult
    }
}
