using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class PlayersPageViewModel : BaseViewModel
{
    private DataProvider DataProvider { get; set; }
    private Tournament Tournament { get; set; }
    private Team? Team { get; set; }

    public string Header => Team != null ? $"Players of {Team}" : "Players";
    public ObservableCollection<Player> Players { get; private set; }

    public RelayCommand AddPlayerCommand { get; set; }

    public PlayersPageViewModel(
        DataProvider dataProvider,
        Tournament tournament,
        Team? team)
    {
        DataProvider = dataProvider;
        Tournament = tournament;
        Team = team;
        Players = DataProvider.Players.GetData();

        AddPlayerCommand = new RelayCommand(AddPlayer, _ => true);

        //Filter by Team if Team is chosen
        if (Team != null) Players = new ObservableCollection<Player>(Players.Where(player => player.Team == Team));
    }

    private void AddPlayer(object? obj)
    {
        var addPlayerWindow = new AddPlayerWindow(DataProvider, null);
        addPlayerWindow.Owner = obj as Window;
        addPlayerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        addPlayerWindow.Show();
    }
}