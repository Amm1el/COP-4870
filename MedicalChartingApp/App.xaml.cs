using MedicalChartingApp.Services;

namespace MedicalChartingApp;

public partial class App : Application
{
    // Shared in-memory services for the whole app
    public static IPatientService PatientService { get; } = new InMemoryPatientService();
    public static IPhysicianService PhysicianService { get; } = new InMemoryPhysicianService();
    public static IAppointmentService AppointmentService { get; } = new InMemoryAppointmentService();

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}
