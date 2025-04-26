using PV178_Project.Models.Abstracts;
using PV178_Project.Models.Enums;

namespace PV178_Project.Models;

public class Tournament(
    int id,
    string name,
    Sport sport,
    Format format,
    DateTime startDate,
    DateTime endDate,
    int teamsCount,
    int groups,
    int pointsWin,
    int pointsDraw,
    int pointsLoss
) : BaseModel(id)
{
    public string Name { get; set; } = name;
    public Sport Sport { get; set; } = sport;
    public Format Format { get; set; } = format;
    public DateTime Start { get; set; } = startDate;
    public DateTime End { get; set; } = endDate;
    public int TeamsCount { get; set; } = teamsCount;
    public int GroupsCount { get; set; } = groups;

    public List<string> Groups { get; set; } = Enumerable
        .Range(0, groups)
        .Select(i => ((char)('A' + i)).ToString())
        .ToList();

    public int PointsWin { get; set; } = pointsWin;
    public int PointsDraw { get; set; } = pointsDraw;
    public int PointsLoss { get; set; } = pointsLoss;
}