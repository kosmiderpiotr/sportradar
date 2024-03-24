using System.Collections.Generic;
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
        var match1 = sut.StartNew("Mexico", "Canada");
        var match2 = sut.StartNew("Spain", "Brazil");
        var match3 = sut.StartNew("Germany", "France");
        var match4 = sut.StartNew("Uruguay", "Italy");
        var match5 = sut.StartNew("Argentina", "Australia");

        sut.UpdateScore(match1, 0, 5);
        sut.UpdateScore(match2, 10, 2);
        sut.UpdateScore(match3, 2, 2);
        sut.UpdateScore(match4, 6, 6);
        sut.UpdateScore(match5, 3, 1);


        var inprogressMatchesList = sut.GetInprogressMatches();

        inprogressMatchesList.Should().HaveCount(5);
        inprogressMatchesList[0].Should().Match<Match>(x => x.Id == match4);
        inprogressMatchesList[1].Should().Match<Match>(x => x.Id == match2);
        inprogressMatchesList[2].Should().Match<Match>(x => x.Id == match1);
        inprogressMatchesList[3].Should().Match<Match>(x => x.Id == match5);
        inprogressMatchesList[4].Should().Match<Match>(x => x.Id == match3);
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
    public void RemoveMatchTest()
    {
        var sut = new Scoreboard();

        var matchId1 = sut.StartNew("Mexico", "Canada");
        var matchId2 = sut.StartNew("Mexico", "Canada");

        var matchList = sut.GetInprogressMatches();

        matchList.Should().HaveCount(2);

        sut.RemoveMatch(matchId1);

        matchList = sut.GetInprogressMatches();
        matchList.Should().HaveCount(1);

        matchList.First().Id.Should().Be(matchId2);
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

    public void RemoveMatch(Guid matchId)
    {
        var match = _matches.First(x => x.Id == matchId);

        _matches.Remove(match);
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