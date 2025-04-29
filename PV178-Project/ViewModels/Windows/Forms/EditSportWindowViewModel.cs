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

    private string _name;
    private int _matchLength;

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
        if (Sport != null)
        {
            Name = Sport.Name;
            MatchLength = Sport.MatchLength;
        }

    }

    private void Save(object? obj)
    {
        if (Sport != null)
        {
            Sport.Name = Name;
            Sport.MatchLength = MatchLength;
        }
        else
        {
            DataProvider.Sports.Add(new Sport(0, Name, MatchLength));
        }

        var confirmationWindow = new ConfirmationDialog("Changes Saved");
        confirmationWindow.ShowDialog();
        DialogWindow.Close();
    }

    private bool CanSave(object? obj)
    {
        return Name is not null && MatchLength > 0;
    }
}