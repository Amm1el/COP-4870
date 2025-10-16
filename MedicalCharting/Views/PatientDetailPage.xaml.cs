using MedicalChartingMaui.ViewModels;

namespace MedicalChartingMaui.Views;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class PatientDetailPage : ContentPage
{
    public string? PatientId { get; set; }
    private readonly PatientDetailViewModel _vm;

    public PatientDetailPage(PatientDetailViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var id = Guid.TryParse(PatientId, out var g) ? g : Guid.Empty;
        await _vm.LoadAsync(id);
        if (_vm.Item?.Birthdate is { } d) DobEntry.Text = d.ToString("MM/dd/yyyy");
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_vm.Item != null && DateOnly.TryParse(DobEntry.Text, out var dob))
            _vm.Item.Birthdate = dob;
        await Shell.Current.GoToAsync("..");
    }
}
