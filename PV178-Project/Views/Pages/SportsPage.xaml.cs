using System.Windows.Controls;
using PV178_Project.Services;
using PV178_Project.ViewModels;

namespace PV178_Project.Views.Pages;

public partial class SportsPage : Page
{
    public SportsPage(DataProvider dataProvider)
    {
        InitializeComponent();
        DataContext = new SportPageViewModel(dataProvider);
    }
}