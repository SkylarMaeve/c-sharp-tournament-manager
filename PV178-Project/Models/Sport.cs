
namespace PV178_Project.Models;

public class Sport()
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MatchLength { get; set; }
    public override string ToString()
    {
        return Name;
    }
}