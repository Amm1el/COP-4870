using MedicalChartingMaui.Models;
using MedicalChartingMaui.Services;
using System.Windows.Input;

namespace MedicalChartingMaui.ViewModels;

public class PatientDetailViewModel : BaseViewModel
{
    private readonly IPatientService _svc;
    public Patient? Item { get; private set; }

    public ICommand SaveCommand { get; }

    public PatientDetailViewModel(IPatientService svc)
    {
        _svc = svc;
        SaveCommand = new Command(async () => { if (Item != null) await _svc.UpdateAsync(Item); });
    }

    public async Task LoadAsync(Guid id)
    {
        Item = id == Guid.Empty ? new Patient() : await _svc.GetAsync(id) ?? new Patient();
        OnPropertyChanged(nameof(Item));
    }

    protected void OnPropertyChanged(string name) =>
        base.GetType().GetEvent("PropertyChanged")?.Raise(this, new PropertyChangedEventArgs(name));
}
