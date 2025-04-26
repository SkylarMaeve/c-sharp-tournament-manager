using System.Windows;
using PV178_Project.Services;

namespace PV178_Project.Views.Windows;

public partial class AddTournamentWindow : Window
{
    public AddTournamentWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new TournamentSettingsPage(new DataProvider(), null));
    }
}