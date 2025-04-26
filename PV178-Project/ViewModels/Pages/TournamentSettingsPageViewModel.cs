using System.Windows.Input;
using PV178_Project.Models;
using PV178_Project.Models.Enums;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class TournamentSettingsPageViewModel : BaseViewModel
{
    private readonly DataProvider _dataProvider;
    private Tournament? _selectedTournament;


    public TournamentSettingsPageViewModel(DataProvider dataProvider, Tournament? selectedTournament)
    {
        _selectedTournament = selectedTournament;
        _dataProvider = dataProvider;

        AddNewSportCommand = new RelayCommand(AddSport, Anything);
    }
    //TODO somehow handle creation lmao
    public string? TournamentName
    {
        get
        {   if (_selectedTournament != null) return _selectedTournament.Name;
            return null;
        }
        set
        {
            if (value != null) _selectedTournament.Name = value;
        }
    }
    public Sport? Sport
    {
        get
        {   if (_selectedTournament != null) return _selectedTournament.Sport;
            return null;
        }
        set
        {
            if (_selectedTournament != null) _selectedTournament.Sport = value;
        }
    }
    public Format? GameFormat
    {
        get
        {   if (_selectedTournament != null) return _selectedTournament.Format;
            return null;
        }
        set
        {
            //TODO
        }
    }
    public DateTime? DateFrom
    {
        get
        {   if (_selectedTournament != null) return _selectedTournament.Start;
            return null;
        }
        set
        {
            
        }
    }
    public DateTime? DateTo
    {
        get
        {   if (_selectedTournament != null) return _selectedTournament.End;
            return null;
        }
        set
        {
            
        }
    }

    private int _meh = 4;
    public int TeamsCount
    {
        get
        {    //OVERHAUL
            return _meh;//TODO
        }
        set
        {
            _meh = value;
            OnPropertyChanged(nameof(TeamsCount));
        }
    }
    
    public int Win
    {
        get
        {   if (_selectedTournament != null) return _selectedTournament.PointsWin;
            return 0;
        }
        set
        {
            
        }
    }
    public int Draw
    {
        get
        {   if (_selectedTournament != null) return _selectedTournament.PointsDraw;
            return 0;
        }
        set
        {
            
        }
    }
    public int Loss
    {
        get
        {   if (_selectedTournament != null) return _selectedTournament.PointsLoss;
            return 0;
        }
        set
        {
        
    }
    }

    public ICommand AddNewSportCommand { get; }

    public List<Sport> Sports
    {
        get => _dataProvider.Sports;
    }
    
    public List<Format> Formats
    {
        get
        {
            return Enum.GetValues(typeof(Format)).Cast<Format>().ToList();
        }
        set
        {
            
        }
    }

    private void AddSport(object? obj)
    {
        new AddSportWindow().ShowDialog();
    }

    private bool Anything(object? obj)
    {
        return true;
    }
}