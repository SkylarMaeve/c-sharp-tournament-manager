using System.Windows.Controls;
using PV178_Project.Models;
using PV178_Project.Services;

namespace PV178_Project.Views.Pages;

public partial class MatchSpiderPage : Page
{
    public MatchSpiderPage(DataProvider dataProvider, Tournament tournament)
    {
        InitializeComponent();
    }
}