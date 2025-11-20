using Microsoft.Maui.Controls;

namespace MedicalChartingApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register route used for navigation to patient details (AppShell requirement)
        Routing.RegisterRoute("patientdetail", typeof(Views.PatientDetailPage));
    }
}
