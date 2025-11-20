namespace MedicalChartingApp.Models;

public class Appointment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PatientId { get; set; }
    public Guid PhysicianId { get; set; }

    public DateTime Start { get; set; }            // local start time
    public TimeSpan Length { get; set; } = TimeSpan.FromMinutes(30);
}
