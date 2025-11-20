using MedicalChartingApp.Models;

namespace MedicalChartingApp.Services;

public interface IPatientService
{
    Task<List<Patient>> GetAllAsync();
    Task<Patient?> GetAsync(Guid id);
    Task AddAsync(Patient patient);
    Task UpdateAsync(Patient patient);
    Task DeleteAsync(Guid id);
}
