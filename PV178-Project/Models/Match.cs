using PV178_Project.Models.Abstracts;
using PV178_Project.Models.Enums;

namespace PV178_Project.Models;

public class Match(
    int id, 
    Tournament tournament, 
    string name,
    Team? teamA, 
    Team? teamB, 
    DateTime startTime) : BaseModel(id)
{
    public Tournament Tournament { get; set; } = tournament;
    public Team? TeamA { get; set; } = teamA;
    public Team? TeamB { get; set; } = teamB;
    public DateTime StartTime { get; set; } = startTime;
    public Team? Winner { get; set; } = null;
    public string Name { get; set; } = name;
    public int PointsTeamA { get; set; } = 0;
    public int PointsTeamB { get; set; } = 0;
}