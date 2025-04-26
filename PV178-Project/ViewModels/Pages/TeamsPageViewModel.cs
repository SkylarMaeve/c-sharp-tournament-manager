using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class TeamsPageViewModel : BaseViewModel
{
    private readonly DataProvider _dataProvider;
    private Tournament _selectedTournament;
    private Frame _mainFrame;

    public TeamsPageViewModel(DataProvider dataProvider, Tournament selectedTournament, Frame frame)
    {
        _selectedTournament = selectedTournament;
        _dataProvider = dataProvider;
        _mainFrame = frame;
        //Filter by Teams in Tournament
        Teams = _dataProvider.Teams.Where(t => t.Tournament == _selectedTournament).ToList();
        AddTeamWindowCommand = new RelayCommand(AddTeam, Anything);
        ShowTeamPlayersCommand = new RelayCommand(ExecuteNavigateToPlayers, Anything);
    }

    public ICommand AddTeamWindowCommand { get; }
    public ICommand ShowTeamPlayersCommand { get; }

    public List<Team> Teams { get; private set; }

    private void AddTeam(object? obj)
    {
        new AddTeamWindow().ShowDialog();
    }

    private bool Anything(object? obj)
    {
        return true;
    }
    
    private void PlayersButtonCLick(object? obj)
    {
        Console.WriteLine(obj);
        Console.WriteLine("Tlačidlo");
        _mainFrame.Navigate(new PlayersPage(_dataProvider, _selectedTournament, null));
    }
    
    private void ExecuteNavigateToPlayers(object? parameter)
    {
        if (parameter is Team selectedTeam)
        {
            // Handle navigation logic here (e.g., open Players page)
            Console.WriteLine("pitcho");
            _mainFrame.Navigate(new PlayersPage(_dataProvider, _selectedTournament, selectedTeam));
        }
    }
}