using PV178_Project.Models;
using PV178_Project.Services;

namespace PV178_Project.ViewModels;

public class TournamentSettingsPageViewModel
{
    private Tournament _selectedTournament;
    private readonly DataProvider _dataProvider;
    

    public TournamentSettingsPageViewModel(DataProvider dataProvider, Tournament selectedTournament)
    {
        _selectedTournament = selectedTournament;
        _dataProvider = dataProvider;
    }
}