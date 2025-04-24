using PV178_Project.Models;
using PV178_Project.Services;

namespace PV178_Project.ViewModels;

public class PlayersPageViewModel
{
    private Tournament _selectedTournament;
    private readonly DataProvider _dataProvider;
    
    public List<Player> Players { get; private set; }

    public PlayersPageViewModel(DataProvider dataProvider, Tournament selectedTournament)
    {
        _selectedTournament = selectedTournament;
        _dataProvider = dataProvider;
        //Filter by Teams in Tournament
        Players = _dataProvider.Players;
    }
}