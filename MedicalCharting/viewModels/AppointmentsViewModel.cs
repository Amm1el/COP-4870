using System.Collections.ObjectModel;
using System.Windows.Input;
using MedicalChartingMaui.Models;
using MedicalChartingMaui.Services;

namespace MedicalChartingMaui.ViewModels;

public class AppointmentsViewModel : BaseViewModel
{
    private readonly IAppointmentService _apptSvc;
    private readonly IPatientService _patientSvc;
    private readonly IPhysicianService _physSvc;

    public ObservableCollection<Appointment> Appointments { get; } = new();
    public ObservableCollection<Patient> Patients { get; } = new();
    public ObservableCollection<Physician> Physicians { get; } = new();

    public Appointment NewAppointment { get; set; } = new();

    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }

    public AppointmentsViewModel(IAppointmentService a, IPatientService p, IPhysicianService d)
    {
        _apptSvc = a; _patientSvc = p; _physSvc = d;
        LoadCommand = new Command(async () => await LoadAsync());
        AddCommand = new Command(async () => await AddAsync());
        DeleteCommand = new Command<Guid>(async id => await DeleteAsync(id));
    }

    public async Task LoadAsync()
    {
        Appointments.Clear();
        foreach (var a in await _apptSvc.GetAllAsync())
            Appointments.Add(a);

        Patients.Clear();
        foreach (var p in await _patientSvc.GetAllAsync())
            Patients.Add(p);

        Physicians.Clear();
        foreach (var d in await _physSvc.GetAllAsync())
            Physicians.Add(d);
    }

    private async Task AddAsync()
    {
        if (NewAppointment.PatientId == Guid.Empty || NewAppointment.PhysicianId == Guid.Empty)
            throw new InvalidOperationException("Select patient and physician.");

        if (NewAppointment.Length == TimeSpan.Zero)
            NewAppointment.Length = TimeSpan.FromMinutes(30);

        await _apptSvc.AddAsync(NewAppointment);
        Appointments.Add(NewAppointment);
        NewAppointment = new Appointment(); // reset
        OnPropertyChanged(nameof(NewAppointment));
    }

    private async Task DeleteAsync(Guid id)
    {
        await _apptSvc.DeleteAsync(id);
        var toRemove = Appointments.FirstOrDefault(x => x.Id == id);
        if (toRemove != null) Appointments.Remove(toRemove);
    }
}
