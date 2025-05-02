using System.ComponentModel;
using PV178_Project.Models.Enums;

namespace PV178_Project.Models;

public class Tournament() : INotifyPropertyChanged
{
    private string _name;

    public int Id { get; set; }

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

    public virtual Sport Sport { get; set; }
    public Format Format { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int TeamsCount { get; set; }

    public int PointsWin { get; set; }
    public int PointsDraw { get; set; }
    public int PointsLoss { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}