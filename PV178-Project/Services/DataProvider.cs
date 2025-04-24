using PV178_Project.Models;
using PV178_Project.Models.Relations;
using PV178_Project.Views;

namespace PV178_Project.Services;

public class DataProvider
{
    public List<Player> Players { get; private set; }
    public List<Team> Teams { get; private set; }
    public List<Match> Matches { get; private set; }
    public List<Tournament> Tournaments { get; private set; }
    public List<Participation> Participations { get; private set; }
    public List<Sport> Sports { get; private set; }

    public DataProvider()
    {
        Players = new List<Player>();
        Teams = setTeams();
        Matches = new List<Match>();
        Tournaments = new List<Tournament>();
        Participations = new List<Participation>();
        Sports = new List<Sport>();
        setPlayers();
    }

    private List<Team> setTeams()
    {
        var data =  new List<Team>
        {
            new Team("John Meyer", new Sport("VOle", 45)),
            new Team("John Meyer", new Sport("VOle", 45)),
            new Team("John Meyer", new Sport("VOle", 45)),
        };
        return data;
    }
    private void setPlayers()
    {
         Players = new List<Player>
        {
            new Player("John Meyer", Teams[0], null, new DateTime(2002, 12, 1)),
            new Player("John Meyer", Teams[0], null, new DateTime(2002, 12, 1)),
            new Player("John Meyer", Teams[0], null, new DateTime(2002, 12, 1)),
            new Player("John Meyer", Teams[0], null, new DateTime(2002, 12, 1)),
            new Player("John Meyer", Teams[0], null, new DateTime(2002, 12, 1)),
        };
    }
    
}