using System.Windows.Controls;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.ViewModels;

namespace PV178_Project.Views;

public partial class PlayersPage : Page
{
    public PlayersPage(DataProvider dataProvider, Tournament tournament, Team? team)
    {
        InitializeComponent();
        DataContext = new PlayersPageViewModel(dataProvider, tournament, team);
    }
}