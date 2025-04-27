using System.Windows;

namespace PV178_Project.ViewModels;

public class ConfirmationWindowViewModel
{
    public string Message { get; set; }
    public Window ConfirmationWindow { get; set; }

    public RelayCommand CloseCommand { get; set; }

    public ConfirmationWindowViewModel(string message, Window window)
    {
        Message = message;
        ConfirmationWindow = window;
        CloseCommand = new RelayCommand(CloseWindow, _ => true);
    }

    public void CloseWindow(object? obj) => ConfirmationWindow.Close();
}