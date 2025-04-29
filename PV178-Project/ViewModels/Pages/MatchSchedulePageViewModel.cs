using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class MatchSchedulePageViewModel : BaseViewModel
{
    private DataProvider DataProvider { get; set; }
    private Tournament Tournament { get; set; }

    public ICollectionView Matches { get; private set; }

    public RelayCommand GenerateMatchesCommand { get; }
    public RelayCommand ExportMatchesCommand { get; }
    public RelayCommand EditMatchCommand { get; }
    public RelayCommand AddMatchCommand { get; }

    public MatchSchedulePageViewModel(DataProvider dataProvider, Tournament selectedTournament)
    {
        DataProvider = dataProvider;
        Tournament = selectedTournament;
        Matches = CollectionViewSource.GetDefaultView(DataProvider.Matches.GetData());
        //TODO implement Generator, Exporters
        GenerateMatchesCommand = new RelayCommand(GenerateMatches, _ => true);
        ExportMatchesCommand = new RelayCommand(ExportMatches, _ => true);
        EditMatchCommand = new RelayCommand(EditMatch, _ => true);
        AddMatchCommand = new RelayCommand(AddMatch, _ => true);
        Filter();
    }

    private void Filter()
    {
        Matches.Filter = team =>
        {
            var t = team as Match;
            if (t == null) return false;
            bool a = t.Tournament == Tournament;
            return a;
        };
        Matches.Refresh();
    }

    private void GenerateMatches(object? parameter)
    {
        var window = new GenerateMatchesWindow(Tournament, DataProvider);
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.ShowDialog();
        //Sort by date
        Matches.SortDescriptions.Add(new SortDescription(nameof(Match.StartTime), ListSortDirection.Ascending));
        Matches.Refresh();
    }

    private void ExportMatches(object? parameter)
    {
        //TODO Static Match Exporter(DataProvider, Tournament)
    }

    private void EditMatch(object? parameter)
    {
        var window = new EditMatchWindow(DataProvider, (parameter is Match match) ? match : null, Tournament);
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.ShowDialog();
        Matches.Refresh();
    }
    
    private void AddMatch(object? parameter)
    {
        var window = new EditMatchWindow(DataProvider, null, Tournament);
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.ShowDialog();
        Matches.Refresh();
    }
}