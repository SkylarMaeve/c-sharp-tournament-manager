using System.Windows;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.ViewModels.Windows;

namespace PV178_Project.Views.Windows;

public partial class AddTeamWindow : Window
{
    public AddTeamWindow(DataProvider dataProvider, Tournament tournament,Team? team)
    {
        InitializeComponent();
        DataContext = new EditTeamWindowViewModel(dataProvider, tournament,team, this);
    }
}