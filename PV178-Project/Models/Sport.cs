namespace PV178_Project.Models;

public class Sport
{
    public Sport(string sportName, int matchLength)
    {
        SportName = sportName;
        MatchLength = matchLength;
    }

    public string SportName { get; private set; }
    public int MatchLength { get; private set; }

    public bool ChangeSportName(string newSportName)
    {
        SportName = newSportName;
        return true;
    }

    public bool ChangeMatchLength(int newMatchLength)
    {
        if (newMatchLength <= 0) return false;
        MatchLength = newMatchLength;
        return true;
    }

    public override string ToString()
    {
        return SportName;
    }
}