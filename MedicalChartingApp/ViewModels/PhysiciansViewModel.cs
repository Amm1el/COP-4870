using System.Collections.ObjectModel;
using System.Windows.Input;
using MedicalChartingApp.Models;
using MedicalChartingApp.Services;

namespace MedicalChartingApp.ViewModels;

public class PhysiciansViewModel : BaseViewModel
{
    private readonly IPhysicianService _svc;

    public ObservableCollection<Physician> Physicians { get; } = new();

    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }

    public PhysiciansViewModel() : this(App.PhysicianService) { }

    public PhysiciansViewModel(IPhysicianService svc)
    {
        _svc = svc;
        LoadCommand = new Command(async () => await LoadAsync());
        AddCommand = new Command(async () => await AddAsync());
        DeleteCommand = new Command<Guid>(async id => await DeleteAsync(id));
    }

    public async Task LoadAsync()
    {
        Physicians.Clear();
        foreach (var d in await _svc.GetAllAsync())
            Physicians.Add(d);
    }

    private async Task AddAsync()
    {
        var d = new Physician { Name = "New Physician" };
        await _svc.AddAsync(d);
        Physicians.Add(d);
    }

    private async Task DeleteAsync(Guid id)
    {
        await _svc.DeleteAsync(id);
        var toRemove = Physicians.FirstOrDefault(x => x.Id == id);
        if (toRemove != null)
            Physicians.Remove(toRemove);
    }
}
