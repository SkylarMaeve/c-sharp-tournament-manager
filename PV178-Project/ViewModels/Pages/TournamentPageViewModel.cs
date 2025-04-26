using System.Windows.Input;
using PV178_Project.Models;
using PV178_Project.Models.Enums;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class TournamentPageViewModel : BaseViewModel
{
    private DataProvider DataProvider { get; set; }
    private Tournament Tournament { get; set; }
    public string TournamentName { get; set; }
    public Sport Sport { get; set; }
    public Format Format { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int TeamsCount { get; set; }
    public int GroupsCount { get; set; }
    public List<string> Groups => Tournament.Groups;
    public int Win { get; set; }
    public int Draw { get; set; }
    public int Loss { get; set; }
    public List<Sport> Sports => DataProvider.Sports.GetData().ToList();

    public List<Format> Formats
    {
        get => Enum.GetValues(typeof(Format)).Cast<Format>().ToList();
        set => Format = value.First();
    }

    public RelayCommand AddSportCommand { get; set; }
    public RelayCommand SaveChangesCommand { get; set; }

    public TournamentPageViewModel(
        DataProvider dataProvider,
        Tournament tournament)
    {
        Tournament = tournament;
        DataProvider = dataProvider;

        //Initialize Properties
        TournamentName = Tournament.Name;
        Sport = Tournament.Sport;
        Format = Tournament.Format;
        DateFrom = Tournament.Start;
        DateTo = Tournament.End;
        TeamsCount = Tournament.TeamsCount;
        GroupsCount = Tournament.GroupsCount;
        Win = Tournament.PointsWin;
        Draw = Tournament.PointsDraw;
        Loss = Tournament.PointsLoss;

        AddSportCommand = new RelayCommand(AddSport, _ => true);
        SaveChangesCommand = new RelayCommand(SaveChanges, CanSaveChanges);
    }

    private void AddSport(object? obj) => new AddSportWindow().ShowDialog();

    private void SaveChanges(object? obj)
    {
        Tournament.Name = TournamentName;
        Tournament.Sport = Sport;
        Tournament.Format = Format;
        Tournament.Start = DateFrom;
        Tournament.End = DateTo;
        Tournament.TeamsCount = TeamsCount;
        Tournament.GroupsCount = GroupsCount;
        Tournament.PointsWin = Win;
        Tournament.PointsDraw = Draw;
        Tournament.PointsLoss = Loss;
    }

    private bool CanSaveChanges(object? obj)
    {
        //TODO Basically Return All is not null
        return true;
    }
}