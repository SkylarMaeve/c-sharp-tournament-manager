using PV178_Project.Models.Abstracts;

namespace PV178_Project.Models;

public class Sport(int id, string name, int matchLength): BaseModel(id)
{
    public string Name { get; set; } = name;
    public int MatchLength { get; set; } = matchLength;
    public override string ToString()
    {
        return Name;
    }
}