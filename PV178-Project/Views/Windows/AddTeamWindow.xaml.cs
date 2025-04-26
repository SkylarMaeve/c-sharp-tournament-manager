using System.Windows;
using PV178_Project.ViewModels.Windows;

namespace PV178_Project.Views.Windows;

public partial class AddTeamWindow : Window
{
    public AddTeamWindow()
    {
        InitializeComponent();
        DataContext = new AddTeamWindowViewModel();
    }
}