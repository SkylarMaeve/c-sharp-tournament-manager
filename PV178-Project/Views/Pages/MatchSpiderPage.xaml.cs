using System.Windows.Controls;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.ViewModels;

namespace PV178_Project.Views.Pages;

public partial class MatchSpiderPage : Page
{
    public MatchSpiderPage(DataProvider dataProvider, Tournament tournament)
    {
        InitializeComponent();
        DataContext = new MatchSpiderPageViewModel(dataProvider, tournament);
    }
}