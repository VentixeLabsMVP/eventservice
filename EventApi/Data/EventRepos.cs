using Microsoft.EntityFrameworkCore;

namespace EventApi.Data
{
    public  class EventRepos(EventDbContext context)
    {
        private readonly EventDbContext _context = context;
    
        public async Task<List<EventEntity>> GetAllEventsAsync()
        {
            return await _context.Events.ToListAsync();
        }
    }
}
