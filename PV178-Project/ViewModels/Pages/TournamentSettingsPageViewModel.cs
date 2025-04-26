using PV178_Project.Models;
using PV178_Project.Services;

namespace PV178_Project.ViewModels;

public class TournamentSettingsPageViewModel
{
    private readonly DataProvider _dataProvider;
    private Tournament _selectedTournament;


    public TournamentSettingsPageViewModel(DataProvider dataProvider, Tournament selectedTournament)
    {
        _selectedTournament = selectedTournament;
        _dataProvider = dataProvider;
    }
}