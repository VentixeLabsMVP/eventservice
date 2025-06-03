using EventApi.Models;
using EventApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace EventApi.Controllers;

[Route("api/event")]
[ApiController]
public class EventsController(EventServices eventService, AddressService addressService) : ControllerBase
{
    private readonly EventServices _eventService = eventService;
    private readonly AddressService _addressService = addressService;

    [HttpGet("/")]
    public IActionResult RedirectToEvent()
    {
        return Redirect("/api/event");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var result = await _eventService.GetAllAsync();
        return Ok(result); // ok(return) method in controllerBase. is a part of Iactionresult
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvent(int id)
    {
        var result = await _eventService.GetAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventsDto dto)
    {
        if (dto.Address != null)
        {
            var addressId = await _addressService.CreateAsync(dto.Address);
            dto.AddressId = addressId;
        }

        var result = await _eventService.CreateEventAsync(dto);
        if (result == null)
            return BadRequest("Could not create event.");

        return CreatedAtAction(nameof(GetEvent), new { id = result.Id }, result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventsDto dto)
    {
        if (id != dto.Id)
            return BadRequest("ID mismatch.");


        if (dto.Address != null)
        {
            var updatedAddress = await _addressService.UpdateAsync(dto.Address);
            if (updatedAddress == null)
                return NotFound("Address not found.");

            dto.AddressId = updatedAddress.Id;
        }

        var result = await _eventService.UpdateEventAsync(dto);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var success = await _eventService.DeleteEventAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
