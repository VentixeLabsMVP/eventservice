using EventApi.Data;
using EventApi.Entities;
using EventApi.Models;

namespace EventApi.Service;

public class AddressService(AddressRepos addressRepos)
{
    private readonly AddressRepos _addressRepos = addressRepos;

    public async Task<int> CreateAsync(AddressDto dto)
    {
        if (dto == null)
            return 0;

        var entity = new Address
        {
            StreetName = dto.StreetName,
            City = dto.City
        };

        var result = await _addressRepos.CreateAsync(entity);
        return result.Id ?? 0;
    }

    public async Task<AddressDto?> GetByIdAsync(int id)
    {
        var result = await _addressRepos.GetByIdAsync(id);
        if (result == null)
            return null;

        return new AddressDto
        {
            Id = result.Id,
            StreetName = result.StreetName,
            City = result.City
        };
    }

    public async Task<IEnumerable<AddressDto>> GetAllAsync()
    {
        var result = await _addressRepos.GetAllAsync();

        return result.Select(a => new AddressDto
        {
            Id = a.Id,
            StreetName = a.StreetName,
            City = a.City
        });
    }

    public async Task<AddressDto?> UpdateAsync(AddressDto dto)
    {
        if (dto == null)
            return null;

        var entity = new Address
        {
            Id = dto.Id,
            StreetName = dto.StreetName,
            City = dto.City
        };

        var result = await _addressRepos.UpdateAsync(entity);
        if (result == null)
            return null;

        return new AddressDto
        {
            Id = result.Id,
            StreetName = result.StreetName,
            City = result.City
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _addressRepos.DeleteAsync(id);
    }
}
