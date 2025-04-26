using System.Windows.Controls;
using System.Windows.Input;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly Frame _mainFrame;
    private readonly DataProvider _dataProvider;
    private Tournament? _selectedTournament = null;
    private List<Tournament> _tournaments;

    public MainViewModel(Frame mainFrame)
    {
        _mainFrame = mainFrame;
        _dataProvider = new DataProvider();
        // Initialize commands
        NavigateToSettingsCommand = new RelayCommand(NavigateToSettings, Anything);
        NavigateToGamePlanCommand = new RelayCommand(NavigateToGamePlan, Anything);
        NavigateToTeamsCommand = new RelayCommand(NavigateToTeams, Anything);
        NavigateToPlayersCommand = new RelayCommand(NavigateToPlayers, Anything);
        LoadTournamentCommand = new RelayCommand(LoadTournament, Anything);
        AddTournamentCommand = new RelayCommand(AddTournament, Anything);
        // Populate tournaments
        Tournaments = _dataProvider.Tournaments;
        _mainFrame.Navigate(new WelcomePage());
    }

    public List<Tournament> Tournaments
    {
        get => _tournaments;
        set
        {
            _tournaments = value;
            OnPropertyChanged(nameof(Tournaments));
        }
    }

    public Tournament? SelectedTournament
    {
        get => _selectedTournament;
        set
        {
            Console.WriteLine(value);
            _selectedTournament = value;
            IsButtonEnabled = _selectedTournament != null;
            OnPropertyChanged(nameof(SelectedTournament));
            LoadTournamentCommand.Execute(null);
        }
    }

    private bool _isButtonEnabled;
    public bool IsButtonEnabled
    {
        get => _isButtonEnabled;
        set
        {
            _isButtonEnabled = value;
            OnPropertyChanged(nameof(IsButtonEnabled));
        }
    }

    

    public ICommand NavigateToSettingsCommand { get; }
    public ICommand NavigateToRankingCommand { get; }
    public ICommand NavigateToGamePlanCommand { get; }
    public ICommand NavigateToTeamsCommand { get; }
    public ICommand NavigateToPlayersCommand { get; }
    public ICommand LoadTournamentCommand { get; }
    public ICommand AddTournamentCommand { get; }

    private void NavigateToSettings(object? obj)
    {
        _mainFrame.Navigate(new TournamentSettingsPage(_dataProvider, SelectedTournament));
    }
    

    private void NavigateToGamePlan(object? obj)
    {
        _mainFrame.Navigate(new GamePlanPage(_dataProvider, SelectedTournament));
    }

    private void NavigateToTeams(object? obj)
    {
        _mainFrame.Navigate(new TeamsPage(_dataProvider, SelectedTournament, _mainFrame));
    }

    private void NavigateToPlayers(object? obj)
    {
        _mainFrame.Navigate(new PlayersPage(_dataProvider, SelectedTournament, null));
    }

    private void LoadTournament(object? obj)
    {
        if (SelectedTournament != null)
            _mainFrame.Navigate(new TournamentSettingsPage(_dataProvider, SelectedTournament));
        else _mainFrame.Navigate(new WelcomePage());
    }

    private void AddTournament(object? obj)
    {
        new AddTournamentWindow().ShowDialog();

    }


    private bool CanAccesData(object? obj)
    {
        return SelectedTournament != null;
    }

    private bool Anything(object? obj)
    {
        return true;
    }
}