using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class SportPageViewModel : BaseViewModel
{
    private DataProvider DataProvider { get; }
    public ICollectionView Sports { get; private set; }
    public RelayCommand EditSportCommand { get; }
    public RelayCommand DeleteSportCommand { get; }

    public SportPageViewModel(DataProvider dataProvider)
    {
        
        DataProvider = dataProvider;
        Sports = CollectionViewSource.GetDefaultView(dataProvider.Sports.GetAll());
        EditSportCommand = new RelayCommand(EditSport, _ => true);
        DeleteSportCommand = new RelayCommand(DeleteSport, _ => true);

    }
    
    private void EditSport(object? parameter)
    {
        new EditSportWindow(
            DataProvider,
            (parameter is Sport team) ? team : null
        ).ShowDialog();
        Sports.Refresh();
    }
    private async void DeleteSport(object? parameter)
    {
        if (parameter is Sport sport)
        {
            //Removing Associated Tournaments first, to avoid confusion between UI and DB
            var tournaments = DataProvider.Tournaments.GetAll().Where(t => t.Sport == sport).ToList();
            foreach (var tournament in tournaments)
            {
                await DataProvider.Tournaments.Remove(tournament);
            }

            await DataProvider.Sports.Remove(sport);
            var confirmationWindow = new ConfirmationDialog("Sport Deleted");

        }
        
    }
}