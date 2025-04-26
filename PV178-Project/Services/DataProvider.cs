using PV178_Project.Models;
using PV178_Project.Models.Enums;

namespace PV178_Project.Services;

public class DataProvider()
{
    public BaseManagerModel<Player> Players { get; private set; } = new BaseManagerModel<Player>();
    public BaseManagerModel<Team> Teams { get; private set; } = new BaseManagerModel<Team>();
    public BaseManagerModel<Match> Matches { get; private set; } = new BaseManagerModel<Match>();
    public BaseManagerModel<Tournament> Tournaments { get; private set; } = new BaseManagerModel<Tournament>();
    public BaseManagerModel<Sport> Sports { get; private set; } = new BaseManagerModel<Sport>();
    
    //Initializes Managers
    public void Initialize()
    {
        SetSports();
        SetTournaments();
        SetTeams();
        SetPlayers();
    }
    private void SetSports()
    {
        var data =  new List<Sport>
        {
            new Sport(0, "Hockey", 60),
            new Sport(1, "FImon", 60),
            new Sport(2, "Basketball", 60),
        };
        foreach (var value in data)
        {
            Sports.Add(value);
        }
    }
    private void SetTeams()
    {
        var data =  new List<Team>
        {
            new Team(0,"Team A",Tournaments.GetData()[0], "A"),
            new Team(1,"Team B",Tournaments.GetData()[0], "A"),
            new Team(2,"Team C",Tournaments.GetData()[0], "A"),
        };
        foreach (var value in data)
        {
            Teams.Add(value);
        }
    }
    private void SetPlayers()
    {
         var data = new List<Player>
        {
            new Player(0,"John 1", Teams.GetData()[0], new DateTime(2002, 12, 1)),
            new Player(1,"John 2", Teams.GetData()[0], new DateTime(2002, 12, 1)),
            new Player(2,"John 3", Teams.GetData()[1], new DateTime(2002, 12, 1)),
            new Player(3,"John 4", Teams.GetData()[1], new DateTime(2002, 12, 1)),
            new Player(4,"John 5", Teams.GetData()[2], new DateTime(2002, 12, 1)),
        };
        foreach (var value in data)
        {
            Players.Add(value);
        }
    }

    private void SetTournaments()
    {
        var data =  new List<Tournament>
        {
            new Tournament(0, "FImonWorld CHampionship",  Sports.GetData()[0], Format.GroupsAndPlayOff, DateTime.Now, DateTime.Now, 2, 1, 0),
            new Tournament(1, "Dunno",  Sports.GetData()[0], Format.GroupsAndPlayOff, DateTime.Now, DateTime.Now, 2, 1, 0),
            new Tournament(2, "Dunno",  Sports.GetData()[1], Format.GroupsAndPlayOff, DateTime.Now, DateTime.Now, 2, 1, 0),
            new Tournament(3, "Dunno",  Sports.GetData()[2], Format.GroupsAndPlayOff, DateTime.Now, DateTime.Now, 2, 1, 0),
            new Tournament(4, "Dunno",  Sports.GetData()[1], Format.GroupsAndPlayOff, DateTime.Now, DateTime.Now, 2, 1, 0),
        };
        foreach (var value in data)
        {
            Tournaments.Add(value);
        }

    }
}