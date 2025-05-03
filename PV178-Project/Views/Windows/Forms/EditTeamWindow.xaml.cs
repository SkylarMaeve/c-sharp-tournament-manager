using System.Windows;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.ViewModels.Windows;

namespace PV178_Project.Views.Windows;

public partial class EditTeamWindow : Window
{
    public EditTeamWindow(DataProvider dataProvider, Tournament tournament,Team? team)
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        DataContext = new EditTeamWindowViewModel(dataProvider, tournament,team, this);
    }
}