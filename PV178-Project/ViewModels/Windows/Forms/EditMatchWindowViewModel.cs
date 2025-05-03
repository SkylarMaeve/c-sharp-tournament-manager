using System.Collections.ObjectModel;
using System.Windows;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels.Windows;

public class EditMatchWindowViewModel : BaseViewModel
{
    private string? _name;

    public string? Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(CanSaveChanges));
        }
    }

    private Team? _teamA;
    private Team? _teamB;

    public Team? TeamA
    {
        get => _teamA;
        set
        {
            _teamA = value;
            OnPropertyChanged(nameof(CanSaveResult));
            OnPropertyChanged(nameof(CanSaveChanges));
        }
    }

    public Team? TeamB
    {
        get => _teamB;
        set
        {
            _teamB = value;
            OnPropertyChanged(nameof(CanSaveResult));
            OnPropertyChanged(nameof(CanSaveChanges));
        }
    }

    public DateTime StartTime { get; set; }
    public Team? Winner { get; set; }
    private Match? Match { get; set; }
    public int PointsTeamA { get; set; }
    public int PointsTeamB { get; set; }

    //9 AM seems reasonable to me
    public int Hour { get; set; } = 9;
    public int Minute { get; set; } = 00;

    public List<int> Hours { get; set; } = new List<int>(Enumerable.Range(00, 24));
    public List<int> Minutes { get; set; } = new List<int>(Enumerable.Range(00, 60));

    public bool CanSaveResult
    {
        get =>
            Winner == null && CanSaveChanges && Match != null;
        set { }
    }

    public bool CanSaveChanges
    {
        get =>
            TeamA != null && TeamB != null && TeamA != TeamB && Name != null;
    }


    public ObservableCollection<Team> Teams { get; set; }
    private DataProvider DataProvider { get; set; }
    private Window DialogWindow { get; set; }
    private Tournament Tournament { get; set; }

    public RelayCommand SaveChangesCommand { get; set; }
    public RelayCommand SaveResultsCommand { get; set; }

    public EditMatchWindowViewModel(DataProvider dataProvider, Match? match, Tournament tournament, Window window)
    {
        SaveResultsCommand = new RelayCommand(SaveResult, _ => true);
        SaveChangesCommand = new RelayCommand(SaveChanges, _ => true);
        DataProvider = dataProvider;
        StartTime = DateTime.Today;
        DialogWindow = window;
        Teams = dataProvider.Teams.GetAll();
        Tournament = tournament;

        //When Editing
        if (match != null)
        {
            Match = match;
            Name = match.Name;
            TeamA = match.TeamA;
            TeamB = match.TeamB;
            StartTime = match.StartTime;
            Hour = StartTime.Hour;
            Minute = StartTime.Minute;
            Winner = match.Winner;
            PointsTeamA = match.PointsTeamA;
            PointsTeamB = match.PointsTeamB;
        }

        OnPropertyChanged(nameof(CanSaveResult));
        OnPropertyChanged(nameof(CanSaveChanges));
    }

    private async void SaveResult(object? obj)
    {
        
        var match = new Match
        {
            Tournament = Tournament,
            TeamA = TeamA,
            TeamB = TeamB,
            Name = Name,
            StartTime = StartTime,
            Winner = Winner,
            PointsTeamA = PointsTeamA,
            PointsTeamB = PointsTeamB
        };


        if (PointsTeamA > PointsTeamB)
        {
            match.Winner = TeamA;
            match.TeamA.Points += Tournament.PointsWin;
            match.TeamB.Points += Tournament.PointsLoss;
            match.TeamA.Wins += 1;
            match.TeamB.Losses += 1;
        }

        if (PointsTeamA < PointsTeamB)
        {
            match.Winner = TeamB;
            match.TeamB.Points += Tournament.PointsWin;
            match.TeamA.Points += Tournament.PointsLoss;
            match.TeamB.Wins += 1;
            match.TeamA.Losses += 1;
        }

        if (PointsTeamA == PointsTeamB)
        {
            match.TeamA.Points += Tournament.PointsDraw;
            match.TeamB.Points += Tournament.PointsDraw;
            match.TeamA.Draws += 1;
            match.TeamB.Draws += 1;
        }


        await DataProvider.Matches.Update(match, Match);
        //Also update Team Scores
        await DataProvider.Teams.Update(match.TeamA, match.TeamA);
        await DataProvider.Teams.Update(match.TeamB, match.TeamB);

        var confirmationDialog = new ConfirmationDialog("Results saved");
        DialogWindow.Close();
    }

    private async void SaveChanges(object? obj)
    {
        if (Match == null)
        {
            StartTime = StartTime.AddHours(Hour).AddMinutes(Minute);
        }
        else
        {
            StartTime = StartTime.Date.AddHours(Hour).AddMinutes(Minute);
        }

        var newMatch = new Match
        {
            Tournament = Tournament,
            TeamA = TeamA,
            TeamB = TeamB,
            Name = Name,
            StartTime = StartTime
        };

        if (Match != null)
            await DataProvider.Matches.Update(newMatch, Match);
        else
            await DataProvider.Matches.Add(newMatch);
        var confirmationDialog = new ConfirmationDialog("Changes saved");
        DialogWindow.Close();
    }
}