using System.Diagnostics.Metrics;
using FluentAssertions;

namespace FootballWorldCupScoreboard.Tests;

public class ScoreboardTests
{
    [Fact]
    public void GetInprogressMatchesTest()
    {
        var sut = new Scoreboard();
        sut.StartNew("Mexico", "Canada");
        sut.StartNew("Spain", "Brazil");
        sut.StartNew("Germany", "France");
        sut.StartNew("Uruguay", "Italy");
        sut.StartNew("Argentina", "Australia");

        var inprogressMatchesList = sut.GetInprogressMatches();

        inprogressMatchesList.Should().HaveCount(5);
    }
}

public class Scoreboard
{
    private List<Match> _matches { get; init; } = [];

    public void StartNew(string homeTeam, string awayTeam)
    {
        var newMath = new Match(homeTeam, awayTeam);

        _matches.Add(newMath);
    }

    public List<Match> GetInprogressMatches()
    {
        return _matches;
    }
}

public class Match
{
    public string HomeTeam { get; }

    public string AwayTeam { get; }

    public Match(string homeTeam, string awayTeam)
    {
        HomeTeam = homeTeam;

        AwayTeam = awayTeam;
    }
}