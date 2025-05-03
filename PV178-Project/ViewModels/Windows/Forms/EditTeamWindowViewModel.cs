using System.Windows;
using PV178_Project.Models;
using PV178_Project.Models.Enums;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels.Windows;

public class EditTeamWindowViewModel : BaseViewModel

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

    public string? GroupName
    {
        get => _group;
        set
        {
            _group = value;
            SaveTeamCommand.RaiseCanExecuteChanged();
        }
    }

    public int? Place { get; set; }
    public List<int> Places { get; set; }
    public List<string> Groups { get; set; }
    public RelayCommand SaveTeamCommand { get; }

    public EditTeamWindowViewModel(
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
        Groups = Enumerable
            .Range(0, Tournament.Format == Format.PlayOff ? 2 : 1)
            .Select(i => ((char)('A' + i)).ToString())
            .ToList();
        Places = Enumerable.Range(1, Tournament.TeamsCount).ToList();
        if (Team != null)
        {
            Name = Team.Name;
            GroupName = Team.GroupName;
            Place = Team.Placement;
        }
    }

    private async void Save(object? obj)
    {
        var newTeam = new Team
        {
            Name = Name,
            GroupName = GroupName,
            Tournament = Tournament,
            Placement = Place
        };
        if (Team != null)
        {
            newTeam.Wins = Team.Wins;
            newTeam.Losses = Team.Losses;
            newTeam.Draws = Team.Draws;
            newTeam.Points = Team.Points;
            await DataProvider.Teams.Update(newTeam, Team);
        }
        else
        {
            await DataProvider.Teams.Add(newTeam);
        }


        var confirmationWindow = new ConfirmationDialog("Changes Saved");
        DialogWindow.Close();
    }

    private bool CanSave(object? obj) => Name is not null && GroupName is not null;
}