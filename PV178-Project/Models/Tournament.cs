using PV178_Project.Models.Abstracts;
using PV178_Project.Models.Enums;

namespace PV178_Project.Models;

public class Tournament : Entity
{
    public Tournament(long id, string tournamentName, TournamentFormat format, int pointsWin, int pointsTie,
        int pointsLoss, DateTime startDate, DateTime endDate, Sport sport) : base(id)
    {
        //Groups
        //Count
        TournamentName = tournamentName;
        Format = format;
        PointsWin = pointsWin;
        PointsTie = pointsTie;
        PointsLoss = pointsLoss;
        StartDate = startDate;
        EndDate = endDate;
        Sport = sport;
        Teams = new List<Team>();
        Matches = new List<Match>();
        Ongoing = false;
    }

    public string TournamentName { get; set; }
    public TournamentFormat Format { get; set; }
    public int PointsWin { get;  set; }
    public int PointsTie { get;  set; }
    public int PointsLoss { get;  set; }
    public DateTime StartDate { get;  set; }
    public DateTime EndDate { get;  set; }
    public Sport Sport { get;  set; }
    public List<Team> Teams { get; }
    public List<Match> Matches { get; }
    public bool Ongoing { get; set; }

    public bool AddTeam(Team team)
    {
        if (Teams.Contains(team)) return false;
        Teams.Add(team);
        return true;
    }

    public bool RemoveTeam(Team team)
    {
        if (!Teams.Contains(team)) return false;
        Teams.Remove(team);
        return true;
    }

    public bool AddMatch(Match match)
    {
        if (!Matches.Contains(match)) return false;
        Matches.Add(match);
        return true;
    }
}