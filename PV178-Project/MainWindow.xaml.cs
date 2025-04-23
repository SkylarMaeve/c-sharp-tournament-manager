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
using PV178_Project.Models;
using PV178_Project.Models.Enums;
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
        
        var data = new List<Tournament>
        {
            new Tournament(0l, "Select", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now , DateTime.Now, new Sport("vole", 56)),

            new Tournament(0l, "FImon", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now , DateTime.Now, new Sport("vole", 56)),
            new Tournament(1l, "Poker", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now , DateTime.Now, new Sport("vole", 56)),
            new Tournament(2l, "Kys", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now , DateTime.Now, new Sport("vole", 56)),
            new Tournament(3l, "Vole", TournamentFormat.GroupsAndPlayOff, 2, 1, 0, DateTime.Now , DateTime.Now, new Sport("vole", 56)),
        };
        //TODO Strings
        
        ComboBox.ItemsSource = data;
        var meh = ComboBox.SelectedIndex;
        if (meh != 0) Console.WriteLine(meh);
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
    private void LoadTournament(object sender, RoutedEventArgs e)
    {
        if (ComboBox.SelectedIndex != 0) MainFrame.Navigate(new TournamentSettingsPage());
        if (ComboBox.SelectedIndex == 0) MainFrame.Navigate(new WelcomePage());
    }
    private void AddTOurnament(object sender, RoutedEventArgs e)
    {
        if (ComboBox.SelectedIndex != 0) MainFrame.Navigate(new TournamentSettingsPage());
    }
}

