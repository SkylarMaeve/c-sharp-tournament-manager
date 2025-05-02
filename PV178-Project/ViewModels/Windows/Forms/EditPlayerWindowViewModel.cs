using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels.Windows;

public class EditPlayerWindowViewModel : BaseViewModel
{
    private DataProvider DataProvider { get; set; }
    private Window DialogWindow { get; set; }

    private string? _name;
    private Team? _team;
    private DateTime? _dateOfBirth;

    public string? Name
    {
        get => _name;
        set
        {
            _name = value;
            SavePlayerCommand.RaiseCanExecuteChanged();
        }
    }

    public Team? Team
    {
        get => _team;
        set
        {
            _team = value;
            SavePlayerCommand.RaiseCanExecuteChanged();
        }
    }

    public DateTime? DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            _dateOfBirth = value;
            SavePlayerCommand.RaiseCanExecuteChanged();
        }
    }

    public string Header => Player != null ? "Edit Player" : "Add Player";


    private Player? Player { get; set; }

    public ObservableCollection<Team> Teams { get; set; }
    public RelayCommand SavePlayerCommand { get; set; }

    public EditPlayerWindowViewModel(DataProvider dataProvider, Tournament tournament ,Player? player, Window window)
    {
        DataProvider = dataProvider;
        DialogWindow = window;
        Teams = DataProvider.Teams.GetAll();
        Teams = new ObservableCollection<Team>(Teams.Where(t => t.Tournament == tournament));

        if (player != null)
        {
            Name = player.Name;
            Team = player.Team;
            DateOfBirth = player.DateOfBirth;
        }

        SavePlayerCommand = new RelayCommand(Save, CanSave);
    }

    private async void Save(object? obj)
    {

        var newPlayer = new Player { Name = Name, Team = Team, DateOfBirth = DateOfBirth.Value };
        if (Player != null)
        {
            await DataProvider.Players.Update(newPlayer, Player);
        }
        else
        {
            await DataProvider.Players.Add( new Player { Name = Name, Team = Team, DateOfBirth = DateOfBirth.Value });
        }

        var confirmationWindow = new ConfirmationDialog("Changes Saved");
        confirmationWindow.ShowDialog();

        DialogWindow.Close();
    }

    private bool CanSave(object? obj)
    {
        return Name is not null && Team is not null && DateOfBirth is not null && DateOfBirth < DateTime.Now;
    }
}