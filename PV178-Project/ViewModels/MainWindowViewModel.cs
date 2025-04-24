using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using PV178_Project.Models;
using PV178_Project.Models.Enums;
using PV178_Project.Services;
using PV178_Project.Views;

namespace PV178_Project.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly Frame _mainFrame;
        private List<Tournament> _tournaments;
        private Tournament _selectedTournament;
        private  DataProvider _dataProvider;

        public List<Tournament> Tournaments
        {
            get => _tournaments;
            set
            {
                _tournaments = value;
                OnPropertyChanged(nameof(Tournaments));
            }
        }

        public Tournament SelectedTournament
        {
            get => _selectedTournament;
            set
            {
                _selectedTournament = value;
                OnPropertyChanged(nameof(SelectedTournament));
                LoadTournament();
            }
        }

        public CustomCommand NavigateToSettingsCommand { get; }
        public CustomCommand NavigateToRankingCommand { get; }
        public CustomCommand NavigateToGamePlanCommand { get; }
        public CustomCommand NavigateToTeamsCommand { get; }
        public CustomCommand NavigateToPlayersCommand { get; }
        public CustomCommand LoadTournamentCommand { get; }
        public CustomCommand AddTournamentCommand { get; }

        public MainViewModel(Frame mainFrame)
        {
            _mainFrame = mainFrame;
            _dataProvider = new DataProvider();
            Console.WriteLine(_dataProvider.Players);
            // Initialize commands
            NavigateToSettingsCommand = new CustomCommand(NavigateToSettings);
            NavigateToRankingCommand = new CustomCommand(NavigateToRanking);
            NavigateToGamePlanCommand = new CustomCommand(NavigateToGamePlan);
            NavigateToTeamsCommand = new CustomCommand(NavigateToTeams);
            NavigateToPlayersCommand = new CustomCommand(NavigateToPlayers);
            LoadTournamentCommand = new CustomCommand(LoadTournament);
            AddTournamentCommand = new CustomCommand(AddTournament);

            // Populate tournaments
            Tournaments = new List<Tournament>
            {
                new Tournament(0L, "Select", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now, DateTime.Now, new Sport("vole", 56)),
                new Tournament(1L, "FImon", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now, DateTime.Now, new Sport("vole", 56)),
                new Tournament(2L, "Poker", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now, DateTime.Now, new Sport("vole", 56)),
                new Tournament(3L, "Kys", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now, DateTime.Now, new Sport("vole", 56)),
                new Tournament(4L, "Vole", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now, DateTime.Now, new Sport("vole", 56)),
            };

            SelectedTournament = Tournaments[0]; // Default selection
            LoadTournament();
        }

        private void NavigateToSettings() => _mainFrame.Navigate(new TournamentSettingsPage(_dataProvider, SelectedTournament));
        private void NavigateToRanking() => _mainFrame.Navigate(new RankingPage(_dataProvider, SelectedTournament));
        private void NavigateToGamePlan() => _mainFrame.Navigate(new GamePlanPage(_dataProvider, SelectedTournament));
        private void NavigateToTeams() => _mainFrame.Navigate(new TeamsPage(_dataProvider, SelectedTournament));
        private void NavigateToPlayers() => _mainFrame.Navigate(new PlayersPage(_dataProvider, SelectedTournament));
        
        private void LoadTournament()
        {
            if (SelectedTournament.getId() != 0) _mainFrame.Navigate(new TournamentSettingsPage(_dataProvider, SelectedTournament));
            else _mainFrame.Navigate(new WelcomePage());
        }
        
        private void AddTournament()
        {
            if (SelectedTournament.getId() != 0) _mainFrame.Navigate(new TournamentSettingsPage(_dataProvider, SelectedTournament));
        }
    }
}
