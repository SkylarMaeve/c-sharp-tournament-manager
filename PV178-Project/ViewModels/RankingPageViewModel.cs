using PV178_Project.Models;
using PV178_Project.Services;

namespace PV178_Project.ViewModels;

public class RankingPageViewModel : BaseViewModel
{
    private Tournament _selectedTournament;
    private readonly DataProvider _dataProvider;
    
    public List<Team> Teams { get; private set; }

    public RankingPageViewModel(DataProvider dataProvider, Tournament selectedTournament)
    {
        _selectedTournament = selectedTournament;
        _dataProvider = dataProvider;
        //Filter by Teams in Tournament
        
        Teams = _dataProvider.Teams.OrderBy(team => team.Points).ToList();
        
    }
}