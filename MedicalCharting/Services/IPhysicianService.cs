using MedicalChartingMaui.Models;

namespace MedicalChartingMaui.Services;

public interface IPhysicianService
{
    Task<List<Physician>> GetAllAsync();
    Task<Physician?> GetAsync(Guid id);
    Task AddAsync(Physician physician);
    Task UpdateAsync(Physician physician);
    Task DeleteAsync(Guid id);
}
