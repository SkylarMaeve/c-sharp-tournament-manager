using PV178_Project.Models.Abstracts;
using PV178_Project.Models.Enums;

namespace PV178_Project.Models;

public class Match(
    int id, 
    Tournament tournament, 
    Team teamA, 
    Team teamB, 
    DateTime startTime) : BaseModel(id)
{
    public Tournament Tournament { get; set; } = tournament;
    public Team TeamA { get; private set; } = teamA;
    public Team TeamB { get; private set; } = teamB;
    public DateTime StartTime { get; set; } = startTime;
    public Team? Winner { get; set; } = null;
}