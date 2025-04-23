using System.Windows.Controls;
using PV178_Project.Models;

namespace PV178_Project.Views;

public partial class PlayersPage : Page
{
    public PlayersPage()
    {
        InitializeComponent();
        //TODO actual data
        
        var data = new List<Player>
        {
            new Player("John Meyer", null, null, new DateTime(2002, 12, 1)),
            new Player("John Meyer", null, null, new DateTime(2002, 12, 1)),
            new Player("John Meyer", null, null, new DateTime(2002, 12, 1)),
            new Player("John Meyer", null, null, new DateTime(2002, 12, 1)),
            new Player("John Meyer", null, null, new DateTime(2002, 12, 1)),
            
        };

        // Bind data to DataGrid
        Data.ItemsSource = data;
    }
    
   
}