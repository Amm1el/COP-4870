using MedicalChartingApp.ViewModels;

namespace MedicalChartingApp.Views;

public partial class PatientsPage : ContentPage
{
    public PatientsViewModel VM { get; }

    public PatientsPage()
    {
        InitializeComponent();
        VM = new PatientsViewModel();
        BindingContext = VM;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.LoadAsync();
    }
}
