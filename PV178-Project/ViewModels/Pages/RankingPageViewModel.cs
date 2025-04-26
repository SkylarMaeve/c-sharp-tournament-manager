using PV178_Project.Models;
using PV178_Project.Services;

namespace PV178_Project.ViewModels;

public class RankingPageViewModel : BaseViewModel
{
    private readonly DataProvider _dataProvider;
    private Tournament _selectedTournament;

    public RankingPageViewModel(DataProvider dataProvider, Tournament selectedTournament)
    {
        _selectedTournament = selectedTournament;
        _dataProvider = dataProvider;
        //Filter by Teams in Tournament

        Teams = _dataProvider.Teams.OrderBy(team => team.Points).ToList();
    }

    public List<Team> Teams { get; private set; }
}