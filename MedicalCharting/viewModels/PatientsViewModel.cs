using System.Collections.ObjectModel;
using System.Windows.Input;
using MedicalChartingMaui.Models;
using MedicalChartingMaui.Services;

namespace MedicalChartingMaui.ViewModels;

public class PatientsViewModel : BaseViewModel
{
    private readonly IPatientService _svc;
    public ObservableCollection<Patient> Patients { get; } = new();

    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }

    public PatientsViewModel(IPatientService svc)
    {
        _svc = svc;
        LoadCommand = new Command(async () => await LoadAsync());
        AddCommand = new Command(async () => await AddAsync());
        DeleteCommand = new Command<Guid>(async id => await DeleteAsync(id));
    }

    public async Task LoadAsync()
    {
        Patients.Clear();
        foreach (var p in await _svc.GetAllAsync())
            Patients.Add(p);
    }

    private async Task AddAsync()
    {
        var p = new Patient { Name = "New Patient" };
        await _svc.AddAsync(p);
        Patients.Add(p);
    }

    private async Task DeleteAsync(Guid id)
    {
        await _svc.DeleteAsync(id);
        var toRemove = Patients.FirstOrDefault(x => x.Id == id);
        if (toRemove != null) Patients.Remove(toRemove);
    }
}
