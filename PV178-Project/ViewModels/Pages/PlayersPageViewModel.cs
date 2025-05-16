using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class PlayersPageViewModel : BaseViewModel
{
    private DataProvider DataProvider { get; set; }
    private Tournament Tournament { get; set; }
    private Team? Team { get; set; }

    public string Header => Team != null ? $"Players of {Team}" : "Players";
    public ICollectionView Players { get; set; }
    public RelayCommand EditPlayerCommand { get; set; }
    public RelayCommand DeletePlayerCommand { get; set; }


    public PlayersPageViewModel(
        DataProvider dataProvider,
        Tournament tournament,
        Team? team)
    {
        DataProvider = dataProvider;
        Tournament = tournament;
        Team = team;

        EditPlayerCommand = new RelayCommand(EditPlayer, _ => true);
        DeletePlayerCommand = new RelayCommand(DeletePlayer, _ => true);
        
        Players = CollectionViewSource.GetDefaultView(DataProvider.Players.GetAll());
        Filter();
    }

    private void EditPlayer(object? obj)
    {
        var addPlayerWindow = new EditPlayerWindow(DataProvider, Tournament,(obj is Player player) ? player : null);
        addPlayerWindow.ShowDialog();
        Players.Refresh();
    }

    private async void DeletePlayer(object? obj)
    {
        if (obj is Player player)
        {
            await DataProvider.Players.Remove(player);
            var confirmationWindow = new ConfirmationDialog("Player Deleted");

        }
    }
    
    private void Filter()
    {
        Players.Filter = player =>
        {
            var p = player as Player;
            if (p == null) return false;

            bool a = p.Team.Tournament == Tournament;
            bool b = Team == null || p.Team == Team;

            return a && b;
        };
        Players.Refresh();
    }
}