using System.Windows;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.ViewModels;

namespace PV178_Project.Views.Windows;

public partial class GenerateMatchesWindow : Window
{
    public GenerateMatchesWindow(Tournament tournament, DataProvider dataProvider)
    {
        InitializeComponent();
        DataContext = new GenerateMatchesWindowViewModel(tournament, dataProvider, this);
    }
}