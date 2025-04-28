using System.Windows.Controls;
using PV178_Project.Services;
using PV178_Project.ViewModels;

namespace PV178_Project.Views;

public partial class TournamentsPage : Page
{
    public TournamentsPage(MainViewModel model, DataProvider dataProvider)
    {
        InitializeComponent();
        DataContext = new TournamentsPageViewModel(model, dataProvider);
    }
}