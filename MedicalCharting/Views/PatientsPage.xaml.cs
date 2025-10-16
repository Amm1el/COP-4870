using MedicalChartingMaui.ViewModels;

namespace MedicalChartingMaui.Views;

public partial class PatientsPage : ContentPage
{
    public PatientsViewModel VM { get; }
    public Command GoToDetailCommand { get; }

    public PatientsPage(PatientsViewModel vm)
    {
        InitializeComponent();
        VM = vm;
        BindingContext = VM;

        GoToDetailCommand = new Command<Guid>(async id =>
        {
            await Shell.Current.GoToAsync($"patientdetail?patientId={id}");
        });
        // expose command for XAML binding
        this.SetValue(BindingContextProperty, VM);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.LoadAsync();
    }
}
