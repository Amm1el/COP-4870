namespace MedicalChartingMaui.Models;

public class Physician
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public string LicenseNumber { get; set; } = "";
    public DateOnly? GraduationDate { get; set; }
    public string Specialization { get; set; } = "";
}
