using PV178_Project.Models;
using PV178_Project.Services;

namespace PV178_Project.ViewModels;

public class MatchSpiderPageViewModel
{
    private DataProvider DataProvider { get; set; }
    private Tournament Tournament { get; set; }
    
    public RelayCommand ExportSpiderCommand { get; }


    public MatchSpiderPageViewModel(DataProvider dataProvider, Tournament selectedTournament)
    {
        DataProvider = dataProvider;
        Tournament = selectedTournament;

        //TODO Implement GamePlan Page
    }
}