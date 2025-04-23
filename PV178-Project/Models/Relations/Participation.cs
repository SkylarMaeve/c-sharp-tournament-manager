namespace PV178_Project.Models.Relations;

public class Participation
{
    public Participation(Tournament tournament, Team team, string groupName)
    {
        Tournament = tournament;
        Team = team;
        GroupName = groupName;
        Points = 0;
        Placement = null;
    }

    public Tournament Tournament { get; set; }
    public Team Team { get; set; }

    public string GroupName { get; set; }
    public int Points { get; set; }
    public int? Placement { get; set; }
}