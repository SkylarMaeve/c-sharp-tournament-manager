using System.Collections.ObjectModel;
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
    public RelayCommand DeleteTournamentCommand { get; }

    public TournamentsPageViewModel(MainViewModel model, DataProvider dataProvider)
    {
        ParentModel = model;
        DataProvider = dataProvider;
        Tournaments = CollectionViewSource.GetDefaultView(dataProvider.Tournaments.GetAll());
        EditTournamentCommand = new RelayCommand(EditTournament, _ => true);
        DeleteTournamentCommand = new RelayCommand(DeleteTournament, _ => true);

        //Resetting selected Tournament in MainWindow
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
    private async void DeleteTournament(object? parameter)
    {
        if (parameter is Tournament t)
        {
            await DataProvider.Tournaments.Remove(t);
        }
    }
}