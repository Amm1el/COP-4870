using MedicalChartingMaui.Models;
using MedicalChartingMaui.ViewModels;

namespace MedicalChartingMaui.Views;

public partial class AppointmentsPage : ContentPage
{
    public AppointmentsViewModel VM { get; }
    public Patient? SelectedPatient { get; set; }
    public Physician? SelectedPhysician { get; set; }

    public AppointmentsPage(AppointmentsViewModel vm)
    {
        InitializeComponent();
        VM = vm; BindingContext = VM;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.LoadAsync();
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        try
        {
            if (SelectedPatient is null || SelectedPhysician is null)
            {
                await DisplayAlert("Missing", "Select patient and physician.", "OK");
                return;
            }

            if (!DateTime.TryParse(StartEntry.Text, out var start))
            {
                await DisplayAlert("Invalid", "Enter start as MM/dd/yyyy HH:mm", "OK");
                return;
            }

            var lengthMin = 30;
            if (int.TryParse(LengthEntry.Text, out var m) && m > 0) lengthMin = m;

            VM.NewAppointment = new Appointment
            {
                PatientId = SelectedPatient.Id,
                PhysicianId = SelectedPhysician.Id,
                Start = start,
                Length = TimeSpan.FromMinutes(lengthMin)
            };

            await VM.AddCommand.ExecuteAsync(null);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
