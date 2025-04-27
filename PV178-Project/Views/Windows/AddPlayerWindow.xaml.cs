using System.Windows;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.ViewModels.Windows;

namespace PV178_Project.Views.Windows;

public partial class AddPlayerWindow : Window
{
    public AddPlayerWindow(DataProvider dataProvider, Player? player)
    {
        InitializeComponent();
        DataContext = new AddPlayerWindowViewModel(dataProvider, player,this);
    }
}