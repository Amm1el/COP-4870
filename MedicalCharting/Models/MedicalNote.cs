namespace MedicalChartingMaui.Models;

public class MedicalNote
{
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string Text { get; set; } = ""; // diagnosis/prescription
    public Guid? PhysicianId { get; set; }
}
