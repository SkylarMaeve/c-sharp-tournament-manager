using System.Windows.Controls;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.ViewModels;

namespace PV178_Project.Views;

public partial class TournamentSettingsPage : Page
{
    public TournamentSettingsPage(DataProvider dataProvider, Tournament? tournament)
    {
        InitializeComponent();
        DataContext = new TournamentSettingsPageViewModel(dataProvider, tournament);
    }
}