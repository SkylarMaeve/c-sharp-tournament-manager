using System.Windows.Controls;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.ViewModels;

namespace PV178_Project.Views;

public partial class GamePlanPage : Page
{
    public GamePlanPage(DataProvider dataProvider, Tournament tournament)
    {
        InitializeComponent();
        DataContext = new GamePlanPageViewModel(dataProvider, tournament);
    }
}