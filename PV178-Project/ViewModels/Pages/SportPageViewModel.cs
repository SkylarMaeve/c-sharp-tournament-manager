using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.Views.Windows;

namespace PV178_Project.ViewModels;

public class SportPageViewModel
{
    private DataProvider DataProvider { get; }
    public ICollectionView Sports { get; private set; }
    public RelayCommand EditSportCommand { get; }

    public SportPageViewModel(DataProvider dataProvider)
    {
        DataProvider = dataProvider;
        Sports = CollectionViewSource.GetDefaultView(dataProvider.Sports.GetData());
        EditSportCommand = new RelayCommand(EditSport, _ => true);

    }
    private void EditSport(object? parameter)
    {
        var addSportWindow = new AddSportWindow(
            DataProvider, 
            (parameter is Sport team) ? team : null
        );
        addSportWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        addSportWindow.ShowDialog();
        Sports.Refresh();
    }
}