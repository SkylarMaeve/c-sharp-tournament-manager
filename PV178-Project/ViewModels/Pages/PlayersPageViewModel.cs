using System.Windows.Input;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class PlayersPageViewModel
{
    private readonly DataProvider _dataProvider;
    private Tournament _selectedTournament;
    private Team? _selectedTeam;

    public PlayersPageViewModel(DataProvider dataProvider, Tournament selectedTournament, Team? selectedTeam)
    {
        _selectedTournament = selectedTournament;
        _dataProvider = dataProvider;
        //Filter by Teams in Tournament
        Players = _dataProvider.Players;
        _selectedTeam = selectedTeam;
        if (selectedTeam != null)
        {
            Players = _dataProvider.Players.Where(p => p.Team == selectedTeam).ToList();
        }
        AddPlayerCommand = new RelayCommand(AddPlayer, Anything);
    }

    public List<Player> Players { get; private set; }
    public ICommand AddPlayerCommand { get; }

    public string Header
    {
        get
        {
            if (_selectedTeam != null)
            {
                var team = _selectedTeam.Name;
                return "Players of " + team;
            }

            return "Players";
        }
    }

    private void AddPlayer(object? obj)
    {
        new AddPlayerWindow().ShowDialog();
    }

    private bool Anything(object? obj)
    {
        return true;
    }
}