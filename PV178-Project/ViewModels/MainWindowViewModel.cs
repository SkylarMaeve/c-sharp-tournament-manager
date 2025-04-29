using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views;
using PV178_Project.Views.Pages;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class MainViewModel : BaseViewModel
{
    private DataProvider DataProvider { get; }
    public Frame MainFrame { get; }
    private Tournament? Tournament { get; set; }

    public Tournament? SelectedTournament
    {
        get => Tournament;
        set
        {
            Tournament = value;
            OnPropertyChanged(nameof(IsButtonEnabled));
            OnPropertyChanged(nameof(SelectedTournament));
            LoadTournament(null);
        }
    }

    public bool IsButtonEnabled => Tournament != null;
    public ObservableCollection<Tournament> Tournaments { get; set; }

    public RelayCommand SettingsCommand { get; }
    public RelayCommand MatchScheduleCommand { get; }
    public RelayCommand MatchSpiderCommand { get; }
    public RelayCommand TeamsCommand { get; }
    public RelayCommand PlayersCommand { get; }
    public RelayCommand LoadTournamentCommand { get; }
    public RelayCommand AddTournamentCommand { get; }
    public RelayCommand SportsCommand { get; }
    public RelayCommand TournamentsCommand { get; }

    public MainViewModel(Frame mainFrame)
    {
        MainFrame = mainFrame;
        DataProvider = new DataProvider();
        DataProvider.Initialize();

        SettingsCommand = new RelayCommand(NavigateToSettings, _ => true);
        MatchScheduleCommand = new RelayCommand(NavigateToGamePlan, _ => true);
        TeamsCommand = new RelayCommand(NavigateToTeams, _ => true);
        PlayersCommand = new RelayCommand(NavigateToPlayers, _ => true);
        LoadTournamentCommand = new RelayCommand(LoadTournament, _ => true);
        AddTournamentCommand = new RelayCommand(AddTournament, _ => true);
        SportsCommand = new RelayCommand(NavigateToSports, _ => true);
        TournamentsCommand = new RelayCommand(NavigateToTournaments, _ => true);
        MatchSpiderCommand = new RelayCommand(NavigateToSpider, _ => true);

        Tournaments = DataProvider.Tournaments.GetData();
        MainFrame.Navigate(new TournamentsPage(this, DataProvider));
    }

    private void AddTournament(object? obj)
    {
        MainFrame.Navigate(new TournamentPage(this, DataProvider, null));
    }

    private void NavigateToSettings(object? obj)
    {
        MainFrame.Navigate(new TournamentPage(this, DataProvider, SelectedTournament));
    }

    private void NavigateToGamePlan(object? obj) =>
        MainFrame.Navigate(new MatchSchedulePage(DataProvider, SelectedTournament));

    private void NavigateToTeams(object? obj) =>
        MainFrame.Navigate(new TeamsPage(DataProvider, SelectedTournament, MainFrame));

    private void NavigateToPlayers(object? obj) =>
        MainFrame.Navigate(new PlayersPage(DataProvider, SelectedTournament, null));
    private void NavigateToSports(object? obj) =>
        MainFrame.Navigate(new SportsPage(DataProvider));
    private void NavigateToTournaments(object? obj) =>
        MainFrame.Navigate(new TournamentsPage(this, DataProvider));
    
    private void NavigateToSpider(object? obj) =>
        MainFrame.Navigate(new MatchSpiderPage(DataProvider, SelectedTournament));

    private void LoadTournament(object? obj) =>
        MainFrame.Navigate(new TournamentPage(this, DataProvider, SelectedTournament));
}