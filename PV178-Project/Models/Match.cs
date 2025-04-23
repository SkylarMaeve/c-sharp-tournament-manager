using PV178_Project.Models.Enums;

namespace PV178_Project.Models;

public class Match
{
    public Match(Team teamA, Team teamB, DateTime startTime)
    {
        TeamA = teamA;
        TeamB = teamB;
        StartTime = startTime;
        ScoreTeamA = 0;
        ScoreTeamB = 0;
    }

    public Team TeamA { get; set; }
    public Team TeamB { get; set; }
    public DateTime StartTime { get; set; }
    public int ScoreTeamA { get; private set; }
    public int ScoreTeamB { get; private set; }

    public MatchResult GetMatchResult()
    {
        if (ScoreTeamA > ScoreTeamB) return MatchResult.TeamA;
        if (ScoreTeamA < ScoreTeamB) return MatchResult.TeamB;
        return MatchResult.Tie;
    }

    public void AddPoints(Team team, int points)
    {
        if (team == TeamA) ScoreTeamA += points;
        if (team == TeamB) ScoreTeamB += points;
    }
}