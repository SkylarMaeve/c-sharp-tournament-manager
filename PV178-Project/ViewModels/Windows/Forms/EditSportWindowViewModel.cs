using System.Text.RegularExpressions;
using System.Windows;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels.Windows;

public class EditSportWindowViewModel
{
    private DataProvider DataProvider { get; set; }
    private Window DialogWindow { get; set; }
    private Sport? Sport { get; }

    private string _name = "Sport";
    private int _matchLength = 15;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public int MatchLength
    {
        get => _matchLength;
        set
        {
            _matchLength = value;
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public RelayCommand SaveCommand { get; }

    public EditSportWindowViewModel(
        DataProvider dataProvider,
        Sport? sport,
        Window window)
    {
        SaveCommand = new RelayCommand(Save, CanSave);
        DataProvider = dataProvider;
        Sport = sport;
        DialogWindow = window;
        //When Editing
        if (Sport != null)
        {
            Name = Sport.Name;
            MatchLength = Sport.MatchLength;
        }
    }

    private async void Save(object? obj)
    {
        var newSport = new Sport
        {
            Name = Name,
            MatchLength = MatchLength
        };

        if (Sport != null)
            await DataProvider.Sports.Update(newSport, Sport);
        else
            await DataProvider.Sports.Add(newSport);

        var confirmationWindow = new ConfirmationDialog("Changes Saved");
        DialogWindow.Close();
    }

    private bool CanSave(object? obj) => Name is not null && MatchLength > 0;
}