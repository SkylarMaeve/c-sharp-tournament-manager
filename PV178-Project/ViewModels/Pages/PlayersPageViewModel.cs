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
    public List<Player> Players { get; private set; }

    public RelayCommand AddPlayerCommand { get; set; }

    public PlayersPageViewModel(
        DataProvider dataProvider,
        Tournament tournament,
        Team? team)
    {
        DataProvider = dataProvider;
        Tournament = tournament;
        Team = team;
        Players = DataProvider.Players.GetData().ToList();

        AddPlayerCommand = new RelayCommand(AddPlayer, _ => true);

        //Filter by Team if Team is chosen
        if (Team != null) Players = Players.Where(player => player.Team == Team).ToList();
    }

    private void AddPlayer(object? obj) => new AddPlayerWindow().ShowDialog();
}