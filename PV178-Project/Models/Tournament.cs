using System.ComponentModel;
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
    int pointsWin,
    int pointsDraw,
    int pointsLoss
) : BaseModel(id), INotifyPropertyChanged
{
    private string _name = name;

    public string Name
    {
        get { return _name; }
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    public Sport Sport { get; set; } = sport;
    public Format Format { get; set; } = format;
    public DateTime Start { get; set; } = startDate;
    public DateTime End { get; set; } = endDate;
    public int TeamsCount { get; set; } = teamsCount;
    public List<string> Groups { get; set; } = Enumerable
        .Range(0, format == Format.PlayOff ? 2 : 1)
        .Select(i => ((char)('A' + i)).ToString())
        .ToList();

    public int PointsWin { get; set; } = pointsWin;
    public int PointsDraw { get; set; } = pointsDraw;
    public int PointsLoss { get; set; } = pointsLoss;

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}