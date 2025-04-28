using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views;

namespace PV178_Project.ViewModels;

public class TournamentsPageViewModel : BaseViewModel
{
    private DataProvider DataProvider { get; }
    public ICollectionView Tournaments { get; private set; }
    private MainViewModel ParentModel { get; set; }
    public RelayCommand EditTournamentCommand { get; }

    public TournamentsPageViewModel(MainViewModel model, DataProvider dataProvider)
    {
        ParentModel = model;
        DataProvider = dataProvider;
        Tournaments = CollectionViewSource.GetDefaultView(dataProvider.Tournaments.GetData());
        EditTournamentCommand = new RelayCommand(EditTournament, _ => true);

        model.SelectedTournament = null;
        OnPropertyChanged(nameof(model.SelectedTournament));
    }

    private void EditTournament(object? parameter)
    {
        var page = new TournamentPage(
            ParentModel,
            DataProvider,
            (parameter is Tournament t) ? t : null
        );
        ParentModel.MainFrame.Navigate(page);
    }
}