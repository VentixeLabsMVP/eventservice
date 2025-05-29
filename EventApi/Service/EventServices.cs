using EventApi.Data;
using EventApi.Entities;
using EventApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace EventApi.Service;

public class EventServices(EventRepos eventRepos)
{
    private readonly EventRepos _eventRepos = eventRepos;


    public async Task<EventsDto?> CreateEventAsync(EventsDto eventDto)
    {
        if (eventDto == null)
            return null;

        var entity = new EventEntity
        {
            EventName = eventDto.EventName,
            StartDate = eventDto.StartDate,
            EndDate = eventDto.EndDate,
            Description = eventDto.Description,
            Price = eventDto.Price,
            AddressId = eventDto.AddressId
        };

        var result = await _eventRepos.CreateEventAsync(entity);

        if (result == null)
            return null;

        return new EventsDto
        {
            Id = result.Id,
            EventName = result.EventName,
            StartDate = result.StartDate,
            EndDate = result.EndDate,
            Description = result.Description,
            Price = result.Price,
            AddressId = result.AddressId,
            Address = new AddressDto
            {
                Id = result.Address.Id,
                StreetName = result.Address.StreetName,
                City = result.Address.City
            }
        };
    }

    public async Task<IEnumerable<EventsDto>> GetAllAsync()
    {
        var result = await _eventRepos.GetAllEventsAsync();

        return result.Select(e => new EventsDto
        {
            Id = e.Id,
            EventName = e.EventName,
            StartDate = e.StartDate,
            EndDate = e.EndDate,
            Description = e.Description,
            Price = e.Price,
            AddressId = e.AddressId,
            Address = e.Address == null ? null : new AddressDto//need this to prevent null exception
            {
                Id = e.Address.Id,
                StreetName = e.Address.StreetName,
                City = e.Address.City
            }
        });
    }
    

    public async Task<EventsDto?> GetAsync(int id)
    {
        var result = await _eventRepos.GetEventByIdAsync(id);

        if (result == null)
            return null;

        return new EventsDto
        {
            Id = result.Id,
            EventName = result.EventName,
            StartDate = result.StartDate,
            EndDate = result.EndDate,
            Description = result.Description,
            Price = result.Price,
            AddressId = result.AddressId ?? 1, // Om AddressId är nullable
            Address = result.Address == null
        ? new AddressDto
        {
            Id = 1,
            StreetName = "Okänd gata",
            City = "Okänd stad"
        }
        : new AddressDto
        {
            Id = result.Address.Id,
            StreetName = result.Address.StreetName,
            City = result.Address.City
        }
        };
    }
        

    public async Task<EventsDto?> UpdateEventAsync(EventsDto dto)
    {
        if (dto == null)
            return null;

        var entity = new EventEntity
        {
            Id = dto.Id,
            EventName = dto.EventName,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Description = dto.Description,
            Price = dto.Price,
            AddressId = dto.AddressId
        };

        var result = await _eventRepos.UpdateEntityAsync(entity);
        if (result == null)
            return null;

        return new EventsDto
        {
            Id = result.Id,
            EventName = result.EventName,
            StartDate = result.StartDate,
            EndDate = result.EndDate,
            Description = result.Description,
            Price = result.Price,
            AddressId = result.AddressId,
            Address = result.Address == null ? null : new AddressDto
            {
                Id = result.Address.Id,
                StreetName = result.Address.StreetName,
                City = result.Address.City
            }
        };
    }

    public async Task<bool> DeleteEventAsync(int id)
    {
        var result = await _eventRepos.DeleteEntityAsync(id);
        if (result == true)
            return true;
        return false;
    }
  
}


