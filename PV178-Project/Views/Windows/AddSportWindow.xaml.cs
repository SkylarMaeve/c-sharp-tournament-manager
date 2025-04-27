using System.Windows;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.ViewModels.Windows;

namespace PV178_Project.Views.Windows;

public partial class AddSportWindow : Window
{
    public AddSportWindow(DataProvider dataProvider, Sport? sport)
    {
        InitializeComponent();
        DataContext = new AddSportWindowViewModel(dataProvider, sport, this);
    }
}