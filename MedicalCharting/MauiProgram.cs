using MedicalChartingMaui.Services;
using MedicalChartingMaui.ViewModels;
using MedicalChartingMaui.Views;

namespace MedicalChartingMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        // Services
        builder.Services.AddSingleton<IPatientService, InMemoryPatientService>();
        builder.Services.AddSingleton<IPhysicianService, InMemoryPhysicianService>();
        builder.Services.AddSingleton<IAppointmentService, InMemoryAppointmentService>();

        // ViewModels
        builder.Services.AddTransient<PatientsViewModel>();
        builder.Services.AddTransient<PatientDetailViewModel>();
        builder.Services.AddTransient<PhysiciansViewModel>();
        builder.Services.AddTransient<AppointmentsViewModel>();

        // Views
        builder.Services.AddTransient<PatientsPage>();
        builder.Services.AddTransient<PatientDetailPage>();
        builder.Services.AddTransient<PhysiciansPage>();
        builder.Services.AddTransient<AppointmentsPage>();

        return builder.Build();
    }
}
