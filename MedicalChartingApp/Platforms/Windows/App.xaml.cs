using Microsoft.UI.Xaml;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using MedicalChartingApp;   // to see MauiProgram

namespace MedicalChartingApp.WinUI;

public partial class App : MauiWinUIApplication
{
    public App()
    {
        this.InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
