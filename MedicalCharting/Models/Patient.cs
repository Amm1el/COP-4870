namespace MedicalChartingMaui.Models;

public class Patient
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public DateOnly? Birthdate { get; set; }
    public string Race { get; set; } = "";
    public string Gender { get; set; } = "";
    public List<MedicalNote> Notes { get; set; } = new();
}
