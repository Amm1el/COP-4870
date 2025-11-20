using MedicalChartingApp.Models;

namespace MedicalChartingApp.Services;

public class InMemoryPhysicianService : IPhysicianService
{
    private readonly List<Physician> _items = new();

    public Task<List<Physician>> GetAllAsync()
        => Task.FromResult(_items.OrderBy(d => d.Name).ToList());

    public Task<Physician?> GetAsync(Guid id)
        => Task.FromResult(_items.FirstOrDefault(d => d.Id == id));

    public Task AddAsync(Physician physician)
    {
        _items.Add(physician);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Physician physician)
    {
        var index = _items.FindIndex(d => d.Id == physician.Id);
        if (index >= 0)
            _items[index] = physician;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _items.RemoveAll(d => d.Id == id);
        return Task.CompletedTask;
    }
}
