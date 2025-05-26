using EventApi.Data;
using System.Security.Cryptography.X509Certificates;

namespace EventApi.Service;

public class EventServices(EventRepos eventRepos)
{
    private readonly EventRepos _eventRepos = eventRepos;

    public async Task<List<EventEntity>> GetAllAsync()
    {
        var result = await _eventRepos.GetAllEventsAsync();
        return result ?? [];
    }
    

    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; } = 0;
    }

    public class Address
    {
        public int Id { get; set; }
        public string StreetName { get; set; } = null!;
        public string City { get; set; } = null!;

    }
}



