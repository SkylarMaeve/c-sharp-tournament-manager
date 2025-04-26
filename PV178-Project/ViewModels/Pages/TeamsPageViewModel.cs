using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class TeamsPageViewModel : BaseViewModel
{
    private DataProvider DataProvider { get; }
    private Tournament Tournament { get; }
    private Frame MainFrame { get; }
    public List<Team> Teams { get; private set; }

    public RelayCommand AddTeamCommand { get; }
    public RelayCommand ShowPlayersCommand { get; }

    public TeamsPageViewModel(
        DataProvider dataProvider,
        Tournament selectedTournament,
        Frame frame)
    {
        Tournament = selectedTournament;
        DataProvider = dataProvider;
        MainFrame = frame;

        AddTeamCommand = new RelayCommand(AddTeam, _ => true);
        ShowPlayersCommand = new RelayCommand(NavigateToPlayers, _ => true);

        //Filter by Tournament
        Teams = DataProvider.Teams.GetData().Where(t => t.Tournament == Tournament).ToList();
    }

    private void AddTeam(object? obj) => new AddTeamWindow().ShowDialog();

    private void NavigateToPlayers(object? parameter) =>
        MainFrame.Navigate(parameter is Team team ? new PlayersPage(DataProvider, Tournament, team) : null);
}