using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PV178_Project.Views;

namespace PV178_Project;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new WelcomePage());
    }

    private void NavigateToSettings(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new TournamentSettingsPage());
    }

    private void NavigateToRanking(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new RankingPage());
    }

    private void NavigateToGamePlan(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new GamePlanPage());
    }

    private void NavigateToTeams(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new TeamsPage());
    }

    private void NavigateToPlayers(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new PlayersPage());
    }
}