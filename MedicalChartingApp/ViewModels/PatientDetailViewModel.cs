using System.Collections.ObjectModel;
using System.Windows.Input;
using MedicalChartingApp.Models;
using MedicalChartingApp.Services;

namespace MedicalChartingApp.ViewModels;

public class PatientDetailViewModel : BaseViewModel
{
    private readonly IPatientService _svc;

    private Patient? _item;
    public Patient? Item
    {
        get => _item;
        set => Set(ref _item, value);
    }

    public ObservableCollection<MedicalNote> Notes { get; } = new();

    public ICommand SaveCommand { get; }
    public ICommand AddNoteCommand { get; }

    private string _newNoteText = "";
    public string NewNoteText
    {
        get => _newNoteText;
        set => Set(ref _newNoteText, value);
    }

    public PatientDetailViewModel(IPatientService svc)
    {
        _svc = svc;

        SaveCommand = new Command(async () => await SaveAsync());
        AddNoteCommand = new Command(AddNote);
    }

    public async Task LoadAsync(Guid id)
    {
        if (id == Guid.Empty)
            Item = new Patient();
        else
            Item = await _svc.GetAsync(id) ?? new Patient();

        Notes.Clear();
        if (Item != null)
        {
            foreach (var n in Item.Notes.OrderByDescending(n => n.Timestamp))
                Notes.Add(n);
        }
    }

    private async Task SaveAsync()
    {
        if (Item == null) return;

        if (Item.Id == Guid.Empty)
            Item.Id = Guid.NewGuid();

        // if already stored, update; else add
        var existing = await _svc.GetAsync(Item.Id);
        if (existing == null)
            await _svc.AddAsync(Item);
        else
            await _svc.UpdateAsync(Item);
    }

    private void AddNote()
    {
        if (Item == null) return;
        if (string.IsNullOrWhiteSpace(NewNoteText)) return;

        var note = new MedicalNote
        {
            Text = NewNoteText.Trim(),
            Timestamp = DateTime.Now
        };

        Item.Notes.Add(note);
        Notes.Insert(0, note);
        NewNoteText = "";
    }
}
