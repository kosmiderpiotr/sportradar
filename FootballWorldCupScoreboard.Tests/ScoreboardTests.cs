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

    [Fact]
    public void UpdateScoreTest()
    {
        var sut = new Scoreboard();

        var matchId = sut.StartNew("Mexico", "Canada");

        sut.UpdateScore(matchId, 0, 5);

        var match = sut.GetInprogressMatches().First();

        match.HomeTeamScore.Should().Be(0);
        match.AwayTeamScore.Should().Be(5);
    }
}

public class Scoreboard
{
    private List<Match> _matches { get; init; } = [];

    public Guid StartNew(string homeTeam, string awayTeam)
    {
        var newMath = new Match(homeTeam, awayTeam);

        _matches.Add(newMath);

        return newMath.Id;
    }

    public void UpdateScore(Guid matchId, int homeTeamScore, int awayTeamScore)
    {
        var match = _matches
            .First(x => x.Id == matchId);

        match.HomeTeamScore = homeTeamScore;
        match.AwayTeamScore = awayTeamScore;
    }

    public List<Match> GetInprogressMatches()
    {
        return _matches
            .OrderByDescending(x => x.TotalScore)
            .ThenByDescending(x => x.StartDate)
            .ToList();
    }
}

public class Match
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime StartDate { get; init; } = DateTime.Now;

    public string HomeTeam { get; }

    public string AwayTeam { get; }

    public int HomeTeamScore { get; set; } = 0;

    public int AwayTeamScore { get; set; } = 0;

    public int TotalScore => HomeTeamScore + AwayTeamScore;

    public Match(string homeTeam, string awayTeam)
    {
        HomeTeam = homeTeam;

        AwayTeam = awayTeam;
    }
}