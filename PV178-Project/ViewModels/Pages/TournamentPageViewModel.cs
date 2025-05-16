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
    public RelayCommand AddSportCommand { get; set; }
    
    public RelayCommand SaveChangesCommand { get; set; }
    public string? Name { get; set; } = "New Tournament";
    private Sport? _sport;

    public Sport? Sport
    {
        get => _sport;
        set
        {
            _sport = value;
            SaveChangesCommand.RaiseCanExecuteChanged();
        }
    }
    private Format _format;

    public Format Format
    {
        get => _format;
        set
        {
            _format = value;
        }
    }
    public DateTime DateFrom { get; set; } = DateTime.Today;
    public DateTime DateTo { get; set; } = DateTime.Today;
    public int TeamsCount { get; set; } = 2;
    
    public int Win { get; set; }
    public int Draw { get; set; }
    public int Loss { get; set; }

    private MainViewModel ParentModel { get; set; }
    public ObservableCollection<Sport> Sports => DataProvider.Sports.GetAll();

    public List<Format> Formats
    {
        get => Enum.GetValues(typeof(Format)).Cast<Format>().ToList();
        set => Format = value.First();
    }
    
    

    public TournamentPageViewModel(
        MainViewModel model,
        DataProvider dataProvider,
        Tournament? tournament)
    {
        Tournament = tournament;
        DataProvider = dataProvider;
        ParentModel = model;
        AddSportCommand = new RelayCommand(AddSport, _ => true);
        SaveChangesCommand = new RelayCommand(SaveChanges, CanSave);
        //Initialize Properties
        if (tournament != null)
        {
            Name = Tournament.Name;
            Sport = Tournament.Sport;
            Format = Tournament.Format;
            DateFrom = Tournament.Start;
            DateTo = Tournament.End;
            TeamsCount = Tournament.TeamsCount;
            Win = Tournament.PointsWin;
            Draw = Tournament.PointsDraw;
            Loss = Tournament.PointsLoss;
        }
    }

    private void AddSport(object? obj) =>
         new EditSportWindow(DataProvider, null).ShowDialog();

    private async void SaveChanges(object? obj)
    {
        if (Tournament != null)
        {
            DateFrom = DateFrom.AddHours(9); //9 AM is Nice
        }
        var tournament = new Tournament
        {
            Name = Name,
            Sport = Sport,
            Format = Format,
            Start = DateFrom,
            End = DateTo,
            TeamsCount = TeamsCount,
            PointsWin = Win,
            PointsDraw = Draw,
            PointsLoss = Loss,
            
        };
        
        if (Tournament != null)
        {
            await DataProvider.Tournaments.Update(tournament, Tournament);
        }
        else
        {
            await DataProvider.Tournaments.Add(tournament);
            Tournament = tournament;
        }
        ParentModel.SelectedTournament = Tournament;
        var confirmationWindow = new ConfirmationDialog("Changes Saved");
    }
    private bool CanSave(object? obj) => Sport is not null;

}