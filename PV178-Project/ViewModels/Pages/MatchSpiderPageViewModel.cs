using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class MatchSpiderPageViewModel : BaseViewModel
{
    private DataProvider DataProvider { get; set; }
    private Tournament Tournament { get; set; }
    
    public RelayCommand ExportSpiderCommand { get; }
    
    //Columns
    public ICollectionView MatchesR16A { get; set; }
    public ICollectionView MatchesQfA { get; set; }
    public ICollectionView MatchesSfA { get; set; }
    
    public ICollectionView MatchesR16B { get; set; }
    public ICollectionView MatchesQfB { get; set; }
    public ICollectionView MatchesSfB { get; set; }
    //Final and Third place Match
    public ICollectionView MatchesFinal { get; set; }




    public MatchSpiderPageViewModel(DataProvider dataProvider, Tournament selectedTournament)
    {
        DataProvider = dataProvider;
        Tournament = selectedTournament;
        
        //TODO Implement GamePlan Page
        var matches = DataProvider.Matches.GetData().Where(m => m.Tournament == Tournament);

        //Setup
        MatchesR16A = CollectionViewSource.GetDefaultView(matches.Where(t => t.Name.Contains("Round of 16 A")).ToList());
        MatchesQfA = CollectionViewSource.GetDefaultView(matches.Where(t => t.Name.Contains("Quarterfinals A")).ToList());
        MatchesSfA = CollectionViewSource.GetDefaultView(matches.Where(t => t.Name.Contains("Semifinals A")).ToList());
        
        MatchesR16B = CollectionViewSource.GetDefaultView(matches.Where(t => t.Name.Contains("Round of 16 B")).ToList());
        MatchesQfB = CollectionViewSource.GetDefaultView(matches.Where(t => t.Name.Contains("Quarterfinals B")).ToList());
        MatchesSfB = CollectionViewSource.GetDefaultView(matches.Where(t => t.Name.Contains("Semifinals B")).ToList());
        
        
        //Final and 3rd Place
        List<Match> finalMatches = new List<Match>();
        finalMatches.Add(matches.FirstOrDefault(m => m.Name.Contains("Final")));
        finalMatches.Add(matches.FirstOrDefault(m => m.Name.Contains("3rd Place Match")));
        MatchesFinal = CollectionViewSource.GetDefaultView(finalMatches);
        
        ExportSpiderCommand = new RelayCommand(ExportSpider, _ => true);
    }
    private void ExportSpider(object? obj)
    {
        //TODO Export
    }
}