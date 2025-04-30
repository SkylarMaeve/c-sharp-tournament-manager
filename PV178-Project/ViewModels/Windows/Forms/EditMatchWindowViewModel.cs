
using System.Collections.ObjectModel;
using System.Windows;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels.Windows;

public class EditMatchWindowViewModel: BaseViewModel
{
    public string? Name { get; set; }
    public Team? TeamA { get; set; }
    public Team? TeamB { get; set; }
    public DateTime StartTime { get; set; }
    public Team? Winner { get; set; }
    private Match? Match { get; set; }
    
    public int PointsTeamA { get; set; }
    public int PointsTeamB { get; set; }
    
    public bool CanSaveResult { get; set; } = false;

    
    public ObservableCollection<Team> Teams { get; set; }
    private DataProvider DataProvider { get; set; }
    private Window DialogWindow { get; set; }
    private Tournament Tournament { get; set; }
    
    
    public RelayCommand SaveChangesCommand { get; set; }
    public RelayCommand SaveResultsCommand { get; set; }
    
    private bool NotRecordedYet { get; set; }
    public EditMatchWindowViewModel(DataProvider dataProvider, Match? match, Tournament tournament, Window window)
    {
        SaveResultsCommand = new RelayCommand(SaveResult, _ => true);
        SaveChangesCommand = new RelayCommand(SaveData, _ => true);
        DataProvider = dataProvider;
        StartTime = DateTime.Now;

        if (match != null)
        {
            Match = match;
            Name = match.Name;
            TeamA = match.TeamA;
            TeamB = match.TeamB;
            StartTime = match.StartTime;
            Winner = match.Winner;
            PointsTeamA = match.PointsTeamA;
            PointsTeamB = match.PointsTeamB;
            CanSaveResult = true;
        }
        DialogWindow = window;
        Teams = dataProvider.Teams.GetData();
        Tournament = tournament;
        NotRecordedYet = Winner == null && PointsTeamA == 0 && PointsTeamB == 0;
    }
    
    private void SaveResult(object? obj)
    {
        Match.Name = Name;
        Match.Winner = Winner;
        Match.StartTime = StartTime;
        Match.TeamA = TeamA;
        Match.TeamB = TeamB;
        //Update
        Match.PointsTeamA = PointsTeamA;
        Match.PointsTeamB = PointsTeamB;
        
        if (NotRecordedYet)
        {
            if (PointsTeamA > PointsTeamB)
            {
                Match.Winner = TeamA;
                TeamA.Points += Tournament.PointsWin;
                TeamB.Points += Tournament.PointsLoss;
                TeamA.Wins += 1;
                TeamB.Losses += 1;

            }
            if (PointsTeamA < PointsTeamB)
            {
                Match.Winner = TeamB;
                TeamB.Points += Tournament.PointsWin;
                TeamA.Points += Tournament.PointsLoss;
                TeamB.Wins += 1;
                TeamA.Losses += 1;
            }

            if (PointsTeamA == PointsTeamB)
            {
                TeamA.Points += Tournament.PointsDraw;
                TeamB.Points += Tournament.PointsDraw;
                Winner = TeamA; //TODO THink of something better, ENUM
                TeamA.Draws += 1;
                TeamB.Draws += 1;
            }
        }
        

        var confirmationWindow = new ConfirmationDialog("Changes Saved");
        confirmationWindow.ShowDialog();
        DialogWindow.Close();

    }
    
    private void SaveData(object? obj)
    {
        var match = new Match(0, Tournament, Name, TeamA, TeamB, StartTime);
        if (Match != null) DataProvider.Matches.Update(match, Match);
        else DataProvider.Matches.Add(match);
        
        var confirmationWindow = new ConfirmationDialog("Changes Saved");
        confirmationWindow.ShowDialog();
        DialogWindow.Close();

    }
}