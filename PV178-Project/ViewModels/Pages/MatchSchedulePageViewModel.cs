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

    public bool CanGenerate
    {
        get => Matches.Cast<object>().Count() == 0;
        set { }
    }

    public RelayCommand GenerateMatchesCommand { get; }
    public RelayCommand ExportMatchesCommand { get; }
    public RelayCommand EditMatchCommand { get; }
    public RelayCommand DeleteMatchCommand { get; }

    public MatchSchedulePageViewModel(DataProvider dataProvider, Tournament selectedTournament)
    {
        DataProvider = dataProvider;
        Tournament = selectedTournament;
        Matches = CollectionViewSource.GetDefaultView(DataProvider.Matches.GetAll());

        GenerateMatchesCommand = new RelayCommand(GenerateMatches, _ => true);
        ExportMatchesCommand = new RelayCommand(ExportMatches, _ => true);
        EditMatchCommand = new RelayCommand(EditMatch, _ => true);
        DeleteMatchCommand = new RelayCommand(DeleteMatch, _ => true);
        Filter();
        //Sort by date
        Matches.SortDescriptions.Add(new SortDescription(nameof(Match.StartTime), ListSortDirection.Ascending));
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
        window.ShowDialog();
        Matches.Refresh();
        OnPropertyChanged(nameof(CanGenerate));
    }

    private async void ExportMatches(object? parameter) =>
        await TournamentExportService.ExportTournamentMatchesSchedule(Matches.Cast<Match>().ToList(), Tournament.Name);

    private void EditMatch(object? parameter)
    {
        var window = new EditMatchWindow(DataProvider, (parameter is Match match) ? match : null, Tournament);
        window.ShowDialog();
        Matches.Refresh();
        OnPropertyChanged(nameof(CanGenerate));
    }

    private async void DeleteMatch(object? parameter)
    {
        if (parameter is Match match)
        {
            await DataProvider.Matches.Remove(match);
        }

        OnPropertyChanged(nameof(CanGenerate));
    }
}