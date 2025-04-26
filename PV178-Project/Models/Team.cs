using PV178_Project.Models.Abstracts;

namespace PV178_Project.Models;

public class Team(
    int id, 
    string name, 
    Tournament tournament, 
    string group) : BaseModel(id)
{
    public string Name { get; set; } = name;
    public Tournament Tournament { get; set; } = tournament;
    public string Group { get; set; } = group;

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