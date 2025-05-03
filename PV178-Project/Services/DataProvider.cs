using Microsoft.EntityFrameworkCore;
using PV178_Project.Data;
using PV178_Project.Models;
using PV178_Project.Models.Enums;
using Microsoft.EntityFrameworkCore.Proxies;

namespace PV178_Project.Services;

public class DataProvider
{
    //Common UI Managers
    public BaseManagerModel<Player> Players { get; private set; }
    public BaseManagerModel<Team> Teams { get; private set; }
    public BaseManagerModel<Match> Matches { get; private set; }
    public BaseManagerModel<Tournament> Tournaments { get; private set; }
    public BaseManagerModel<Sport> Sports { get; private set; }

    //Database access Services
    private BaseService<Sport> SportService { get; set; }
    private BaseService<Tournament> TournamentService { get;  set; }
    private BaseService<Team> TeamService { get;  set; }
    private BaseService<Match> MatchService { get;  set; }
    private BaseService<Player> PlayerService { get;  set; }

    public DataProvider()
    {
        //DB
        var options = new DbContextOptionsBuilder<ProjectDbContext>()
            .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PV178_Project;Trusted_Connection=True")
            .Options;
        var context = new ProjectDbContext(options);
        
        context.Database.EnsureCreated();
        //DB services
        SportService = new BaseService<Sport>(context);
        TournamentService = new BaseService<Tournament>(context);
        TeamService = new BaseService<Team>(context);
        MatchService = new BaseService<Match>(context);
        PlayerService = new BaseService<Player>(context);
        // UI manager setups
        Sports = new BaseManagerModel<Sport>(SportService);
        Tournaments = new BaseManagerModel<Tournament>(TournamentService);
        Teams = new BaseManagerModel<Team>(TeamService);
        Matches = new BaseManagerModel<Match>(MatchService);
        Players = new BaseManagerModel<Player>(PlayerService);
    }

    public async void Initialize()
    {
        //Fetches all data From DB and stores it for UI
        var sports = await SportService.GetAllAsync();
        var tournaments = await TournamentService.GetAllAsync();
        var teams = await TeamService.GetAllAsync();
        var matches = await MatchService.GetAllAsync();
        var players = await PlayerService.GetAllAsync();
        
        sports.ForEach(s => Sports.AddToUi(s));
        tournaments.ForEach(t => Tournaments.AddToUi(t));
        teams.ForEach(t => Teams.AddToUi(t));
        matches.ForEach(m => Matches.AddToUi(m));
        players.ForEach(p => Players.AddToUi(p));
    }
}