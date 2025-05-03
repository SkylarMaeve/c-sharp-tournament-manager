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
    public RelayCommand DeleteTeamCommand { get; }
    public RelayCommand ShowPlayersCommand { get; }
    public bool CanAddTeam { get=> Teams.Cast<object>().Count() < Tournament.TeamsCount; set{} }

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
        DeleteTeamCommand = new RelayCommand(DeleteTeam, _ => true);
        
        Teams = CollectionViewSource.GetDefaultView(DataProvider.Teams.GetAll());
        Filter();
        Teams.SortDescriptions.Add(new SortDescription(nameof(Team.Points), ListSortDirection.Descending));
    }
    
    private void EditTeam(object? parameter)
    {
        new EditTeamWindow(
            DataProvider, 
            Tournament, 
            (parameter is Team team) ? team : null
        ).ShowDialog();
        OnPropertyChanged(nameof(CanAddTeam));
        Teams.Refresh();
    }
    

    private void ShowPlayers(object? parameter) =>
        MainFrame.Navigate(parameter is Team team ? new PlayersPage(DataProvider, Tournament, team) : null);

    private async void DeleteTeam(object? parameter)
    {
        if (parameter is Team team)
        {
            //Delete associated players and matches
            var players = DataProvider.Players.GetAll().Where(p => p.Team == team).ToList();
            foreach (var player in players)
            {
                await DataProvider.Players.Remove(player);
            }
            var matches = DataProvider.Matches.GetAll().Where(m => m.TeamA == team || m.TeamB == team).ToList();
            foreach (var match in matches)
            {
                await DataProvider.Matches.Remove(match);
            }
            await DataProvider.Teams.Remove(team);
            OnPropertyChanged(nameof(CanAddTeam));
        }
    }
    
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