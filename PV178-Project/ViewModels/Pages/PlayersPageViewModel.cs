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
    public RelayCommand AddPlayerCommand { get; set; }

    public PlayersPageViewModel(
        DataProvider dataProvider,
        Tournament tournament,
        Team? team)
    {
        DataProvider = dataProvider;
        Tournament = tournament;
        Team = team;

        AddPlayerCommand = new RelayCommand(AddPlayer, _ => true);
        
        Players = CollectionViewSource.GetDefaultView(DataProvider.Players.GetData());
        Filter();
    }

    private void AddPlayer(object? obj)
    {
        var addPlayerWindow = new AddPlayerWindow(DataProvider, Tournament,null);
        addPlayerWindow.Owner = obj as Window;
        addPlayerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        addPlayerWindow.Show();
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