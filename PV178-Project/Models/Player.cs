namespace PV178_Project.Models;

public class Player
{
    public Player(string name, Team? team, string position, DateTime dateOfBirth)
    {
        Name = name;
        Team = team;
        Position = position;
        DateOfBirth = dateOfBirth;
    }

    public string Name { get; set; }
    public Team? Team { get; private set; }
    public string Position { get; private set; }
    public DateTime DateOfBirth { get; private set; }


    public bool ChangeTeam(Team? newTeam)
    {
        if (Team == newTeam) return false;
        if (Team != null) Team.RemovePlayer(this);
        Team = newTeam;
        return true;
    }
}