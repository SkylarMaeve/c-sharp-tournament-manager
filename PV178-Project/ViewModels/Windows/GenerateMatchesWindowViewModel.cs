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
        ActualTeamsCount = DataProvider.Teams.GetAll().Where(t => t.Tournament == Tournament).Count();
        GenerateMatchesCommand = new RelayCommand(GenerateMatches, CanGenerateMatches);
    }

    private void GenerateMatches(object? obj)
    {
        GenerateMatchesService.Generate(Tournament, DataProvider);
        var confirmationWindow = new ConfirmationDialog("Successfully Generated Matches");
        confirmationWindow.ShowDialog();
        DialogWindow.Close();
    }

    private bool CanGenerateMatches(object? obj)
    {
        Console.WriteLine((ExpectedTeamsCount, ActualTeamsCount));
        return ExpectedTeamsCount == ActualTeamsCount;
    }
}