using MedicalChartingApp.ViewModels;

namespace MedicalChartingApp.Views;

public partial class PhysiciansPage : ContentPage
{
    public PhysiciansViewModel VM { get; }

    public PhysiciansPage()
    {
        InitializeComponent();
        VM = new PhysiciansViewModel();
        BindingContext = VM;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.LoadAsync();
    }
}
