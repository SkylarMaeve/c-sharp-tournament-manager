
namespace PV178_Project.Models;

public class Player()
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual Team Team { get; set; }
    public DateTime DateOfBirth { get; set; }
}