using MedicalChartingApp.ViewModels;

namespace MedicalChartingApp.Views;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class PatientDetailPage : ContentPage
{
    public string? PatientId { get; set; }
    private readonly PatientDetailViewModel _vm;

    public PatientDetailPage()
    {
        InitializeComponent();
        _vm = new PatientDetailViewModel();
        BindingContext = _vm;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        Guid id = Guid.Empty;
        if (!string.IsNullOrWhiteSpace(PatientId))
            Guid.TryParse(PatientId, out id);

        await _vm.LoadAsync(id);

        if (_vm.Item?.Birthdate is { } d)
            DobEntry.Text = d.ToString("MM/dd/yyyy");
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_vm.Item != null && DateOnly.TryParse(DobEntry.Text, out var dob))
            _vm.Item.Birthdate = dob;

        if (_vm.SaveCommand.CanExecute(null))
            _vm.SaveCommand.Execute(null);

        await Shell.Current.GoToAsync("..");
    }
}
