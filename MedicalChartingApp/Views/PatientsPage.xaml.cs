using MedicalChartingApp.ViewModels;
using Microsoft.Maui.Controls;

namespace MedicalChartingApp.Views;

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
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VM.LoadAsync();
    }
}
