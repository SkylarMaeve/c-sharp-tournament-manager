using System.Windows;
using PV178_Project.Models;
using PV178_Project.Services;
using PV178_Project.ViewModels.Windows;

namespace PV178_Project.Views.Windows;

public partial class EditMatchWindow : Window
{
    public EditMatchWindow(DataProvider dataProvider, Match? match, Tournament tournament)
    {
        InitializeComponent();
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        DataContext = new EditMatchWindowViewModel(dataProvider, match, tournament,this);
    }
}