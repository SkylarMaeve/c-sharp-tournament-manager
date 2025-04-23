namespace PV178_Project.Models.Abstracts;

public abstract class Entity
{
    protected long id;

    protected Entity(long id)
    {
        this.id = id;
    }

    public long getId()
    {
        return id;
    }

    public void setId(long id)
    {
        this.id = id;
    }
}