using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
    public ICollectionView Teams { get; private set; }
    public RelayCommand EditTeamCommand { get; }
    public RelayCommand ShowPlayersCommand { get; }

    public TeamsPageViewModel(
        DataProvider dataProvider,
        Tournament selectedTournament,
        Frame frame)
    {
        Tournament = selectedTournament;
        DataProvider = dataProvider;
        MainFrame = frame;

        EditTeamCommand = new RelayCommand(EditTeam, _ => true);
        ShowPlayersCommand = new RelayCommand(ShowPlayers, _ => true);
        
        Teams = CollectionViewSource.GetDefaultView(DataProvider.Teams.GetAll());
        Filter();
    }
    
    private void EditTeam(object? parameter)
    {
        var addTeamWindow = new AddTeamWindow(
            DataProvider, 
            Tournament, 
            (parameter is Team team) ? team : null
        );
        addTeamWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        addTeamWindow.ShowDialog();
        Teams.Refresh();
    }
    

    private void ShowPlayers(object? parameter) =>
        MainFrame.Navigate(parameter is Team team ? new PlayersPage(DataProvider, Tournament, team) : null);
    
    private void Filter()
    {
        Teams.Filter = team =>
        {
            var t = team as Team;
            if (t == null) return false;
            bool a = t.Tournament == Tournament;
            return a;
        };
        Teams.Refresh();
    }
}