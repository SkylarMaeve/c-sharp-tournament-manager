using PV178_Project.Models.Enums;

namespace PV178_Project.Models;

public class Match()
{

    public int Id { get; set; }
    public virtual Tournament Tournament { get; set; }
    public virtual Team? TeamA { get; set; }
    public virtual Team? TeamB { get; set; }
    public DateTime StartTime { get; set; }
    public virtual Team? Winner { get; set; }
    public string Name { get; set; }
    public int PointsTeamA { get; set; }
    public int PointsTeamB { get; set; }
}