using System.Windows;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class GenerateMatchesWindowViewModel : BaseViewModel
{
    public RelayCommand GenerateMatchesCommand { get; set; }
    private Tournament Tournament { get; set; }
    private DataProvider DataProvider { get; set; }

    public int ExpectedTeamsCount { get; set; }
    public int ActualTeamsCount { get; set; }
    private Window DialogWindow { get; set; }

    public GenerateMatchesWindowViewModel(Tournament tournament, DataProvider dataProvider, Window window)
    {
        DataProvider = dataProvider;
        Tournament = tournament;
        DialogWindow = window;
        ExpectedTeamsCount = Tournament.TeamsCount;
        ActualTeamsCount = DataProvider.Teams.GetAll().Count(t => t.Tournament == Tournament);
        GenerateMatchesCommand = new RelayCommand(GenerateMatches, CanGenerateMatches);
    }

    private async void GenerateMatches(object? obj)
    {
        bool succes = await GenerateMatchesService.Generate(Tournament, DataProvider);
        string message = succes ? "Matches generated." : "Groups must be equal.";
        var confirmationWindow = new ConfirmationDialog(message);
        DialogWindow.Close();
    }

    private bool CanGenerateMatches(object? obj) => ExpectedTeamsCount == ActualTeamsCount;
}