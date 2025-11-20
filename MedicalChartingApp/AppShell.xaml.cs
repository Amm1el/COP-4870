using Microsoft.Maui.Controls;

namespace MedicalChartingApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("patientdetail", typeof(Views.PatientDetailPage));
    }
}
