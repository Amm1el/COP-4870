using MedicalChartingApp.Services;

namespace MedicalChartingApp;

public partial class App : Application
{
    // Access the MAUI DI container correctly:
    private static IServiceProvider Services =>
        ( (MauiApp)Current!.Handler!.MauiContext!.Services.GetService(typeof(MauiApp))! ).Services;

    public static IPatientService PatientService =>
        Services.GetService(typeof(IPatientService)) as IPatientService
        ?? throw new Exception("IPatientService not registered");

    public static IPhysicianService PhysicianService =>
        Services.GetService(typeof(IPhysicianService)) as IPhysicianService
        ?? throw new Exception("IPhysicianService not registered");

    public static IAppointmentService AppointmentService =>
        Services.GetService(typeof(IAppointmentService)) as IAppointmentService
        ?? throw new Exception("IAppointmentService not registered");


    public App()
    {
        InitializeComponent();

        MainPage = new ContentPage
        {
            Content = new Label
            {
                Text = "MAUI is running 🎉",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            }
        };
    }
}
