using System.Windows;
using PV178_Project.ViewModels;

namespace PV178_Project.Views.Windows;

public partial class ConfirmationDialog : Window
{
    public ConfirmationDialog(string message)
    {
        //TODO Finish Window Content
        InitializeComponent();
        DataContext = new ConfirmationWindowViewModel(message, this);
    }
}