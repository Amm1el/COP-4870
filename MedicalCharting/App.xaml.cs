using Microsoft.Maui.Controls;

namespace MedicalCharting;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell(); // Entry point (defined in AppShell.xaml)
    }
}
