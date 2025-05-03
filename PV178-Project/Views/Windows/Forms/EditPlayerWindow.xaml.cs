using System.Windows;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.ViewModels.Windows;

namespace PV178_Project.Views.Windows;

public partial class EditPlayerWindow : Window
{
    public EditPlayerWindow(DataProvider dataProvider, Tournament tournament, Player? player)
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        DataContext = new EditPlayerWindowViewModel(dataProvider, tournament, player,this);
    }
}