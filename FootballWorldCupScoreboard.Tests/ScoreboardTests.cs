using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
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

    [Fact]
    public void StartNewMatchTest()
    {
        var sut = new Scoreboard();

        sut.StartNew("Mexico", "Canada");

        var match = sut.GetInprogressMatches().First();


        match.AwayTeamScore.Should().Be(0);
        match.HomeTeamScore.Should().Be(0);
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

    public int HomeTeamScore { get; set; } = 0;

    public int AwayTeamScore { get; set; } = 0;

    public Match(string homeTeam, string awayTeam)
    {
        HomeTeam = homeTeam;

        AwayTeam = awayTeam;
    }
}