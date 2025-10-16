namespace MedicalChartingMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("patientdetail", typeof(Views.PatientDetailPage));
    }
}
