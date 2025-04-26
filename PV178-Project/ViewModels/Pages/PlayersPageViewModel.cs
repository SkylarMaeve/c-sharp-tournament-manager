using System.Windows.Input;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class PlayersPageViewModel
{
    private readonly DataProvider _dataProvider;
    private Tournament _selectedTournament;

    public PlayersPageViewModel(DataProvider dataProvider, Tournament selectedTournament)
    {
        _selectedTournament = selectedTournament;
        _dataProvider = dataProvider;
        //Filter by Teams in Tournament
        Players = _dataProvider.Players;
        AddPlayerCommand = new RelayCommand(AddPlayer, Anything);
    }

    public List<Player> Players { get; private set; }
    public ICommand AddPlayerCommand { get; }

    private void AddPlayer(object? obj)
    {
        new AddPlayerWindow().ShowDialog();
    }

    private bool Anything(object? obj)
    {
        return true;
    }
}