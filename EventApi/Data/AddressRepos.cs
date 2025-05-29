using EventApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EventApi.Data;

public class AddressRepos(EventDbContext context)
{
    private readonly EventDbContext _context = context;

    public async Task<Address?> CreateAsync(Address address)
    {
        try
        {
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return address;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in CreateAsync: {ex.Message}");
            return null!;
        }
    }

    public async Task<List<Address>> GetAllAsync()
    {
        try
        {
            return await _context.Addresses.ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in GetAllAsync: {ex.Message}");
            return [];
        }
    }

    public async Task<Address?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in GetByIdAsync: {ex.Message}");
            return null;
        }
    }

    public async Task<Address?> UpdateAsync(Address updated)
    {
        try
        {
            var existing = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == updated.Id);
            if (existing == null) return null;

            existing.StreetName = updated.StreetName;
            existing.City = updated.City;

            await _context.SaveChangesAsync();
            return existing;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in UpdateAsync: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
            if (address == null) return false;

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in DeleteAsync: {ex.Message}");
            return false;
        }
    }
}