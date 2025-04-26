namespace PV178_Project.Models.Abstracts;

public abstract class BaseModel(int id)
{
    public int Id { get; set; } = id;
}