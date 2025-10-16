using MedicalChartingMaui.ViewModels;

namespace MedicalChartingMaui.Views;

public partial class PhysiciansPage : ContentPage
{
    public PhysiciansViewModel VM { get; }
    public PhysiciansPage(PhysiciansViewModel vm)
    {
        InitializeComponent();
        VM = vm; BindingContext = VM;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.LoadAsync();
    }
}
