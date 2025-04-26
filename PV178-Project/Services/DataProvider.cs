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
        Sports.Add(new Sport("Hockey", 60));
        Sports.Add(new Sport("FImon", 60));
        Sports.Add(new Sport("Basketball", 60));
        Sports.Add(new Sport("Pitchi", 60));
        
        Tournaments[0].Sport = Sports[0];
    }

    private List<Team> setTeams()
    {
        var data =  new List<Team>
        {
            new Team("Team A",Tournaments[0], "A"),
            new Team("Team B",Tournaments[0], "A"),
            new Team("Team C",Tournaments[0], "A"),
        };
        Teams = data;
        return data;
    }
    private List<Player> setPlayers()
    {
         var data = new List<Player>
        {
            new Player("John Meyer", Teams[0], "null", new DateTime(2002, 12, 1)),
            new Player("John Meyer", Teams[0], "null", new DateTime(2002, 12, 1)),
            new Player("John Meyer", Teams[0], "null", new DateTime(2002, 12, 1)),
            new Player("John Meyer", Teams[0], "null", new DateTime(2002, 12, 1)),
            new Player("John Meyer", Teams[0], "null", new DateTime(2002, 12, 1)),
        };
         Players = data;
         Teams[0].AddPlayer(data[0]);
        return data;
    }

    private List<Tournament> setTournaments()
    {
        var data =  new List<Tournament>
        {
            new Tournament(0L, "Dunno", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now, DateTime.Now, new Sport("vole", 56)),
            new Tournament(1L, "FImon", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now, DateTime.Now, new Sport("vole", 56)),
            new Tournament(2L, "Poker", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now, DateTime.Now, new Sport("vole", 56)),
            new Tournament(3L, "Kys", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now, DateTime.Now, new Sport("vole", 56)),
            new Tournament(4L, "Vole", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now, DateTime.Now, new Sport("vole", 56)),
        };
        Tournaments = data;
        return data;
    }
}