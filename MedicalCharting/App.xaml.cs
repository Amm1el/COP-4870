using Microsoft.Maui.Controls;

namespace MedicalChartingApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}
