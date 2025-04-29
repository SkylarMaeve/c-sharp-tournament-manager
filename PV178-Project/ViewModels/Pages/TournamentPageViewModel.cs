using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
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
    private Format _format;

    public Format Format
    {
        get => _format;
        set
        {
            _format = value;
            OnPropertyChanged(nameof(Format));
        }
    }
    
    public DateTime DateFrom { get; set; } = DateTime.Now;
    public DateTime DateTo { get; set; } = DateTime.Now;
    public int TeamsCount { get; set; }
    
    public int Win { get; set; }
    public int Draw { get; set; }
    public int Loss { get; set; }

    private MainViewModel ParentModel { get; set; }
    public ObservableCollection<Sport> Sports => DataProvider.Sports.GetData();

    public List<Format> Formats
    {
        get => Enum.GetValues(typeof(Format)).Cast<Format>().ToList();
        set => Format = value.First();
    }

    public RelayCommand AddSportCommand { get; set; }
    public RelayCommand SaveChangesCommand { get; set; }

    public TournamentPageViewModel(
        MainViewModel model,
        DataProvider dataProvider,
        Tournament? tournament)
    {
        Tournament = tournament;
        DataProvider = dataProvider;
        ParentModel = model;

        //Initialize Properties
        if (tournament != null)
        {
            TournamentName = Tournament.Name;
            Sport = Tournament.Sport;
            Format = Tournament.Format;
            DateFrom = Tournament.Start;
            DateTo = Tournament.End;
            TeamsCount = Tournament.TeamsCount;
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

    private void AddSport(object? obj) => new AddSportWindow(DataProvider, null).ShowDialog();

    private void SaveChanges(object? obj)
    {
        var tournament = new Tournament(
            89, //UselessValue
            TournamentName,
            Sport,
            Format,
            DateFrom,
            DateTo,
            TeamsCount,
            Win,
            Draw,
            Loss);
        
        if (Tournament != null)
        {
            DataProvider.Tournaments.Update(tournament, Tournament);
        }
        else
        {
            DataProvider.Tournaments.Add(tournament);
            Tournament = tournament;
        }
        ParentModel.SelectedTournament = Tournament;
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