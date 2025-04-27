using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using PV178_Project.Models;
using PV178_Project.Models.Enums;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class TournamentPageViewModel : BaseViewModel
{
    private DataProvider DataProvider { get; set; }
    private Tournament? Tournament { get; set; }
    public string TournamentName { get; set; } = "New Tournament";
    public Sport Sport { get; set; }
    public Format Format { get; set; }
    public DateTime DateFrom { get; set; }= DateTime.Now;
    public DateTime DateTo { get; set; }= DateTime.Now;
    public int TeamsCount { get; set; }
    public int GroupsCount { get; set; }
    public int Win { get; set; }
    public int Draw { get; set; }
    public int Loss { get; set; }
    public ObservableCollection<Sport> Sports => DataProvider.Sports.GetData();

    public List<Format> Formats
    {
        get => Enum.GetValues(typeof(Format)).Cast<Format>().ToList();
        set => Format = value.First();
    }

    public RelayCommand AddSportCommand { get; set; }
    public RelayCommand SaveChangesCommand { get; set; }

    public TournamentPageViewModel(
        DataProvider dataProvider,
        Tournament? tournament)
    {
        Tournament = tournament;
        DataProvider = dataProvider;

        //Initialize Properties
        if (tournament != null)
        {
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
        }
        else
        {
            Sport = DataProvider.Sports.GetData().First();
        }

        AddSportCommand = new RelayCommand(AddSport, _ => true);
        SaveChangesCommand = new RelayCommand(SaveChanges, CanSaveChanges);
    }

    private void AddSport(object? obj) => new AddSportWindow().ShowDialog();

    private void SaveChanges(object? obj)
    {
        if (Tournament != null)
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
        else
        {
            DataProvider.Tournaments.Add(new Tournament(
                0,
                TournamentName,
                Sport,
                Format,
                DateFrom,
                DateTo,
                TeamsCount,
                GroupsCount,
                Win,
                Draw,
                Loss));
        }
        var confirmationWindow = new ConfirmationDialog("Changes Saved");
        confirmationWindow.Owner = obj as Window;
        confirmationWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        confirmationWindow.ShowDialog();
    }

    private bool CanSaveChanges(object? obj)
    {
        //TODO Change Porperties
        
        //TODO SaveChangesCommand.RaiseCanExecuteChanged();
        return true;
    }
}