using EventApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EventApi.Data
{
    public  class EventRepos(EventDbContext context)
    {
        private readonly EventDbContext _context = context;
    
        public async Task<EventEntity?> CreateEventAsync(EventEntity evententity)
        {
            try
            {
                await _context.Events.AddAsync(evententity);
                await _context.SaveChangesAsync();
                return evententity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in CreateEventAsync: {ex.Message}");
                return null!;
            }
        }


        public async Task<List<EventEntity>> GetAllEventsAsync()
        {
            try
            {
                var result = await _context.Events
                .Include(e => e.Address)
                .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error with GetAllEventsAsync {ex.Message} ");
                return [];
                
            }
        }

        public async Task<EventEntity?> GetEventByIdAsync(int id)
        {
            try
            {
                var result = await _context.Events
                .Include(e => e.Address)
                .FirstOrDefaultAsync(e => e.Id == id);

                if (result == null)
                    return null;
                return result;
            }
            catch (Exception ex)
            {
            Debug.WriteLine($"Error with GetEventByIdAsync {ex.Message}");
                return null;
            }
        }
        public async Task<EventEntity> UpdateEntityAsync(EventEntity eventEntity)
        {
            try
            {
                var existingEntity = await _context.Events.FirstOrDefaultAsync(e => e.Id == eventEntity.Id);

                if (existingEntity == null)
                    return null!;

                _context.Entry(existingEntity).CurrentValues.SetValues(eventEntity);
                await _context.SaveChangesAsync();

                return existingEntity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in UpdateEntityAsync: {ex.Message}");
                return null!;
            }
        }

        public async Task<bool> DeleteEntityAsync (int id)
        {
            try
            {
                var result = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

                if (result == null)
                    return false;

                _context.Events.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in DeleteEntityAsync: {ex.Message}");
                return false;
            }
        }
    }
}
