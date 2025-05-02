using PV178_Project.Models.Enums;
using PV178_Project.Services;

namespace PV178_Project.Models;

public static class GenerateMatchesService
{
    public static void Generate(Tournament tournament, DataProvider dataProvider)
    {
        switch (tournament.Format)
        {
            case Format.PlayOff:
                GeneratePlayOff(tournament, dataProvider);
                return;
            case Format.AllAgainstAll:
                GenerateAllVsAll(tournament, dataProvider);
                return;
            default: //NOTHING
                return;
        }
    }

    private async static void GenerateAllVsAll(Tournament tournament, DataProvider dataProvider)
    {
        var teams = dataProvider.Teams.GetAll().Where(t => t.Tournament == tournament);
        var matchLength = tournament.Sport.MatchLength;
        var date = tournament.Start;
        var matchIndex = 0;
        foreach (var teamA in teams)
        {
            foreach (var teamB in teams)
            {
                if (teamA != teamB)
                {
                    var matchDate = date.AddMinutes(matchLength * matchIndex);
                    await dataProvider.Matches.Add(new Match
                    {
                        Id = 0,
                        Tournament = tournament,
                        TeamA = teamA,
                        TeamB = teamB,
                        Name = $"{teamA} vs {teamB}",
                        StartTime = matchDate
                    });
                    matchIndex++;
                }
            }
        }
    }

    private async static void GeneratePlayOff(Tournament tournament, DataProvider dataProvider)
    {
        var teams = dataProvider.Teams.GetAll().Where(t => t.Tournament == tournament);
        var teamsA = teams.Where(t => t.GroupName == "A").ToList();
        var teamsB = teams.Where(t => t.GroupName == "B").ToList();

        var matchLength = tournament.Sport.MatchLength;
        //Left side
        await GenerateSpiderSide(teamsA, matchLength, dataProvider, tournament, false);
        //Right side
        await GenerateSpiderSide(teamsB, matchLength, dataProvider, tournament, true);

        //Third Place
        await dataProvider.Matches.Add(
            new Match
            {
                Tournament = tournament,
                TeamA = null,
                TeamB = null,
                Name = "3rd Place Match",
                StartTime = tournament.Start.AddMinutes(matchLength * ((teams.Count() / 2) + 1))
            }
        );

        //Final
        await dataProvider.Matches.Add(
            new Match
            {
                Tournament = tournament,
                TeamA = null,
                TeamB = null,
                Name = "Final",
                StartTime = tournament.Start.AddMinutes(matchLength * ((teams.Count() / 2) + 2))
            }
        );
    }

    private async static Task GenerateSpiderSide(List<Team> teams, int matchLength, DataProvider dataProvider,
        Tournament tournament, bool right)
    {
        string[] matchNames = new string[] { "Round of 16", "Quarterfinals", "Semifinals" };
        int[] matches = new int[] { 4, 2, 1 };
        var matchIndex = 0;
        int count = teams.Count;
        var date = tournament.Start;
        var startIndex = GetMatchStartIndex(count);
        //Generate matches for one side
        for (int i = startIndex; i < 3; i++)
        {
            for (int j = 0; j < matches[i]; j++)
            {
                var index = 2 * j;
                Team? teamA = null;
                Team? teamB = null;
                if (index <= count && index + 1 < count && i == startIndex)
                {
                    teamA = teams[index];
                    Console.WriteLine(index + 1);
                    teamB = teams[index + 1];
                }

                var matchDate = date.AddMinutes(matchLength * matchIndex);
                var matchNumber = right ? matches[i] + j + 1 : j + 1;
                var group = right ? "B" : "A";
                await dataProvider.Matches.Add(
                    new Match
                    {
                        Tournament = tournament,
                        TeamA = teamA,
                        TeamB = teamB,
                        Name = $"{matchNames[i]} {group} {matchNumber}",
                        StartTime = matchDate
                    }
                );
                matchIndex++;
            }
        }
    }

    private static int GetMatchStartIndex(int teamsCount)
    {
        if (teamsCount == 8) return 0;
        if (teamsCount == 4) return 1;
        if (teamsCount == 2) return 2;
        return 69; //SHOULD NOT REACH THIS
    }
}