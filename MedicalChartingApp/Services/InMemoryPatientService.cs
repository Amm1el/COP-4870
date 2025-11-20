using MedicalChartingApp.Models;

namespace MedicalChartingApp.Services;

public class InMemoryPatientService : IPatientService
{
    private readonly List<Patient> _items = new();

    public Task<List<Patient>> GetAllAsync()
        => Task.FromResult(_items.OrderBy(p => p.Name).ToList());

    public Task<Patient?> GetAsync(Guid id)
        => Task.FromResult(_items.FirstOrDefault(p => p.Id == id));

    public Task AddAsync(Patient patient)
    {
        _items.Add(patient);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Patient patient)
    {
        var index = _items.FindIndex(p => p.Id == patient.Id);
        if (index >= 0)
            _items[index] = patient;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _items.RemoveAll(p => p.Id == id);
        return Task.CompletedTask;
    }
}
