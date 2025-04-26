using PV178_Project.Models;
using PV178_Project.Models.Enums;

namespace PV178_Project.Services;

public class DataProvider
{
    public List<Player> Players { get; private set; }
    public List<Team> Teams { get; private set; }
    public List<Match> Matches { get; private set; }
    public List<Tournament> Tournaments { get; private set; }
    public List<Sport> Sports { get; private set; }
    
    public DataProvider()
    {
        Players = new List<Player>();
        Teams = new List<Team>();
        Matches = new List<Match>();
        Tournaments = new List<Tournament>();
        Sports = new List<Sport>();
        setTournaments();
        setTeams();
        setPlayers();
        Sports.Add(new Sport(0, "Hockey", 60));
        Sports.Add(new Sport(1, "FImon", 60));
        Sports.Add(new Sport(2,"Basketball", 60));
        Sports.Add(new Sport(3,"Pitchi", 60));
        
        Tournaments[0].Sport = Sports[0];
    }

    private List<Team> setTeams()
    {
        var data =  new List<Team>
        {
            new Team(0,"Team A",Tournaments[0], "A"),
            new Team(1,"Team B",Tournaments[0], "A"),
            new Team(2,"Team C",Tournaments[0], "A"),
        };
        Teams = data;
        return data;
    }
    private List<Player> setPlayers()
    {
         var data = new List<Player>
        {
            new Player(0,"John Meyer", Teams[0], new DateTime(2002, 12, 1)),
            new Player(1,"John Meyer", Teams[0], new DateTime(2002, 12, 1)),
            new Player(2,"John Meyer", Teams[0], new DateTime(2002, 12, 1)),
            new Player(3,"John Meyer", Teams[0], new DateTime(2002, 12, 1)),
            new Player(4,"John Meyer", Teams[0], new DateTime(2002, 12, 1)),
        };
         Players = data;
        return data;
    }

    private List<Tournament> setTournaments()
    {
        var data =  new List<Tournament>
        {
            new Tournament(0, "FImonWorld CHampionship",  new Sport(0, "vole", 56), Format.GroupsAndPlayOff, DateTime.Now, DateTime.Now, 2, 1, 0),
            new Tournament(1, "Dunno",  new Sport(0, "Hickey", 56), Format.GroupsAndPlayOff, DateTime.Now, DateTime.Now, 2, 1, 0),
            new Tournament(2, "Dunno",  new Sport(0, "Hockey", 56), Format.GroupsAndPlayOff, DateTime.Now, DateTime.Now, 2, 1, 0),
            new Tournament(3, "Dunno",  new Sport(0, "Hackey", 56), Format.GroupsAndPlayOff, DateTime.Now, DateTime.Now, 2, 1, 0),
            new Tournament(4, "Dunno",  new Sport(0, "Meh", 56), Format.GroupsAndPlayOff, DateTime.Now, DateTime.Now, 2, 1, 0),
        };
        Tournaments = data;
        return data;
    }
}