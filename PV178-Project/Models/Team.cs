
namespace PV178_Project.Models;

public class Team()
{
    public int Id { get; set; }

    public string Name { get; set; }
    public virtual Tournament Tournament { get; set; }
    public string GroupName { get; set; }

    public int Wins { get; set; } = 0;
    public int Losses { get; set; } = 0;
    public int Draws { get; set; } = 0;
    
    public int Points { get; set; } = 0;
    public int? Placement { get; set; } = null;

    public override string ToString()
    {
        return Name;
    }
}