using MedicalChartingApp.Models;
using MedicalChartingApp.ViewModels;

namespace MedicalChartingApp.Views;

public partial class AppointmentsPage : ContentPage
{
    public AppointmentsViewModel VM { get; }

    public Patient? SelectedPatient { get; set; }
    public Physician? SelectedPhysician { get; set; }

    public AppointmentsPage()
    {
        InitializeComponent();
        VM = new AppointmentsViewModel();
        BindingContext = VM;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.LoadAsync();
    }

    private async void OnAddClicked(object? sender, EventArgs e)
    {
        try
        {
            if (SelectedPatient is null || SelectedPhysician is null)
            {
                await DisplayAlert("Missing data", "Select both a patient and physician.", "OK");
                return;
            }

            if (!DateTime.TryParse(StartEntry.Text, out var start))
            {
                await DisplayAlert("Invalid start", "Use format MM/dd/yyyy HH:mm.", "OK");
                return;
            }

            int minutes = 30;
            if (!string.IsNullOrWhiteSpace(LengthEntry.Text) &&
                int.TryParse(LengthEntry.Text, out var parsed) && parsed > 0)
            {
                minutes = parsed;
            }

            VM.NewAppointment = new Appointment
            {
                PatientId = SelectedPatient.Id,
                PhysicianId = SelectedPhysician.Id,
                Start = start,
                Length = TimeSpan.FromMinutes(minutes)
            };

            VM.AddCommand.Execute(null);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
