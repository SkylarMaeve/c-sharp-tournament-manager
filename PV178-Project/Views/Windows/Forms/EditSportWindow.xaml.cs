using System.Windows;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.ViewModels.Windows;

namespace PV178_Project.Views.Windows;

public partial class EditSportWindow : Window
{
    public EditSportWindow(DataProvider dataProvider, Sport? sport)
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        DataContext = new EditSportWindowViewModel(dataProvider, sport, this);
    }
}