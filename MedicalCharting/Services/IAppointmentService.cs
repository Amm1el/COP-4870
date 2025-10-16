using MedicalChartingMaui.Models;

namespace MedicalChartingMaui.Services;

public interface IAppointmentService
{
    Task<List<Appointment>> GetAllAsync();
    Task<Appointment?> GetAsync(Guid id);
    Task AddAsync(Appointment appt);
    Task UpdateAsync(Appointment appt);
    Task DeleteAsync(Guid id);
}
