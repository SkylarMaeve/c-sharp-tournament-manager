using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class MainViewModel : BaseViewModel
{
    private DataProvider DataProvider { get; }
    private Frame MainFrame { get; }
    private Tournament? Tournament { get; set; }

    public Tournament? SelectedTournament
    {
        get => Tournament;
        set
        {
            Tournament = value;
            OnPropertyChanged(nameof(IsButtonEnabled));
            LoadTournament(null);
        }
    }

    public bool IsButtonEnabled => Tournament != null;
    public ICollectionView Tournaments { get; set; }

    public RelayCommand SettingsCommand { get; }
    public RelayCommand GamePlanCommand { get; }
    public RelayCommand TeamsCommand { get; }
    public RelayCommand PlayersCommand { get; }
    public RelayCommand LoadTournamentCommand { get; }
    public RelayCommand AddTournamentCommand { get; }

    public MainViewModel(Frame mainFrame)
    {
        MainFrame = mainFrame;
        DataProvider = new DataProvider();
        DataProvider.Initialize();

        SettingsCommand = new RelayCommand(NavigateToSettings, _ => true);
        GamePlanCommand = new RelayCommand(NavigateToGamePlan, _ => true);
        TeamsCommand = new RelayCommand(NavigateToTeams, _ => true);
        PlayersCommand = new RelayCommand(NavigateToPlayers, _ => true);
        LoadTournamentCommand = new RelayCommand(LoadTournament, _ => true);
        AddTournamentCommand = new RelayCommand(AddTournament, _ => true);

        Tournaments = CollectionViewSource.GetDefaultView(DataProvider.Tournaments.GetData().ToList());
        MainFrame.Navigate(new WelcomePage());
    }

    private void AddTournament(object? obj)
    {
        MainFrame.Navigate(new TournamentPage(this, DataProvider, null));
        Tournaments.Refresh();
    }

    private void NavigateToSettings(object? obj)
    {
        MainFrame.Navigate(new TournamentPage(this, DataProvider, SelectedTournament));
        Tournaments.Refresh();
    }

    private void NavigateToGamePlan(object? obj) =>
        MainFrame.Navigate(new GamePlanPage(DataProvider, SelectedTournament));

    private void NavigateToTeams(object? obj) =>
        MainFrame.Navigate(new TeamsPage(DataProvider, SelectedTournament, MainFrame));

    private void NavigateToPlayers(object? obj) =>
        MainFrame.Navigate(new PlayersPage(DataProvider, SelectedTournament, null));

    private void LoadTournament(object? obj) =>
        MainFrame.Navigate(new TournamentPage(this, DataProvider, SelectedTournament));
}