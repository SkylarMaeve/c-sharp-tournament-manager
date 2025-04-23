using System.Windows.Controls;
using PV178_Project.Models;

namespace PV178_Project.Views;

public partial class TeamsPage : Page
{
    public TeamsPage()
    {
        InitializeComponent();
        //TODO actual data

        var data = new List<Team>
        {
            new Team("John Meyer", new Sport("VOle", 45)),
            new Team("John Meyer", new Sport("VOle", 45)),
            new Team("John Meyer", new Sport("VOle", 45)),
        };

        // Bind data to DataGrid
        Data.ItemsSource = data;
    }
}