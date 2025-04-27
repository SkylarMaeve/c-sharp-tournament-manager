using System.Windows;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels.Windows;

public class AddTeamWindowViewModel: BaseViewModel

{
    private DataProvider DataProvider { get; set; }
    private Window DialogWindow { get; set; }
    private Tournament Tournament { get; }

    private Team? Team { get; set; }
    
    private string? _name;
    private string? _group;
    
    public string? Name
    {
        get => _name;
        set
        {
            _name = value;
            SaveTeamCommand.RaiseCanExecuteChanged();
        }
    }

    public string? Group
    {
        get => _group;
        set
        {
            _group = value;
            SaveTeamCommand.RaiseCanExecuteChanged();
        }
    }
    public List<string> Groups { get; set; }
    public RelayCommand SaveTeamCommand { get; } 
    public AddTeamWindowViewModel(
        DataProvider dataProvider, 
        Tournament tournament,
        Team? team, 
        Window window)
    {
        SaveTeamCommand = new RelayCommand(Save, CanSave);

        DataProvider = dataProvider;
        Tournament = tournament;
        DialogWindow = window;
        Team = team;
        Groups = Tournament.Groups;
        if (Team != null)
        {
            Name = Team.Name;
            Group = Team.Group;
        }
    }
    
    private void Save(object? obj)
    {
        if (Team != null)
        {
            Team.Name = Name;
            Team.Group = Group;
        }
        else
        {
            DataProvider.Teams.Add(new Team(0, Name, Tournament, Group));
        }
        var confirmationWindow = new ConfirmationDialog("Changes Saved");
        confirmationWindow.ShowDialog();
        DialogWindow.Close();
    }

    private bool CanSave(object? obj)
    {
        return Name is not null && Group is not null ;
    }
}