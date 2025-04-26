using System.Windows.Input;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class TeamsPageViewModel : BaseViewModel
{
    private Tournament _selectedTournament;
    private readonly DataProvider _dataProvider;
    
    public ICommand AddTeamWindowCommand { get; }
    
    public List<Team> Teams { get; private set; }

    public TeamsPageViewModel(DataProvider dataProvider, Tournament selectedTournament)
    {
        _selectedTournament = selectedTournament;
        _dataProvider = dataProvider;
        //Filter by Teams in Tournament
        Teams = _dataProvider.Teams;
        AddTeamWindowCommand = new RelayCommand(AddTeam, Anything);
    }
    
    private void AddTeam(object? obj) => (new AddTeamWindow()).ShowDialog();
    
    private bool Anything(object? obj) => true;

}