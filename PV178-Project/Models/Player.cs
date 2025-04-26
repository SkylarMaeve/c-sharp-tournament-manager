using PV178_Project.Models.Abstracts;

namespace PV178_Project.Models;

public class Player(
    int id,
    string name,
    Team team,
    DateTime dateOfBirth) : BaseModel(id)
{
    public string Name { get; set; } = name;
    public Team Team { get; set; } = team;
    public DateTime DateOfBirth { get; set; } = dateOfBirth;
}