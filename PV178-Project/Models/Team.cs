namespace PV178_Project.Models;

public class Team
{
    public Team(string teamName, Tournament tournament, string groupName)
    {
        TeamName = teamName;
        Players = new List<Player>();
        Points = 0;
        Tournament = tournament;
        GroupName = groupName;
        Wins = 0;
        Losses = 0;
        Draws = 0;
        Placement = null;
    }
    public Tournament Tournament{ get; set; }
    
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Draws { get; set; }
    public string GroupName { get; set; }
    public int Points { get; set; }
    public int? Placement { get; set; }
    public string TeamName { get; private set; }
    public List<Player> Players { get; }
    public Sport Sport { get; private set; }
    

    public bool AddPlayer(Player player)
    {
        if (Players.Contains(player)) return false;
        Players.Add(player);
        player.ChangeTeam(this);
        return true;
    }

    public bool RemovePlayer(Player player)
    {
        if (!Players.Contains(player)) return false;
        Players.Remove(player);
        player.ChangeTeam(null);
        return true;
    }

    public bool ChangeTeamName(string newTeamName)
    {
        TeamName = newTeamName;
        return true;
    }

    public override string ToString()
    {
        return TeamName;
    }
}