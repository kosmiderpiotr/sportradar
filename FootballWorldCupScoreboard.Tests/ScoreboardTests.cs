using FluentAssertions;

namespace FootballWorldCupScoreboard.Tests;

public class ScoreboardTests
{
    [Fact]
    public void GetInprogressMatchesTest()
    {
        var sut = new Scoreboard();
        var match1 = sut.StartNew(new Team(CountryEnum.Mexico), new Team(CountryEnum.Canada));
        var match2 = sut.StartNew(new Team(CountryEnum.Spain), new Team(CountryEnum.Brazil));
        var match3 = sut.StartNew(new Team(CountryEnum.Germany), new Team(CountryEnum.France));
        var match4 = sut.StartNew(new Team(CountryEnum.Uruguay), new Team(CountryEnum.Italy));
        var match5 = sut.StartNew(new Team(CountryEnum.Argentina), new Team(CountryEnum.Australia));

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

        sut.StartNew(new Team(CountryEnum.Mexico), new Team(CountryEnum.Canada));

        var match = sut.GetInprogressMatches().First();

        match.AwayTeamScore.Should().Be(0);
        match.HomeTeamScore.Should().Be(0);
    }

    [Fact]
    public void RemoveMatchTest()
    {
        var sut = new Scoreboard();

        var matchId1 = sut.StartNew(new Team(CountryEnum.Mexico), new Team(CountryEnum.Canada));
        var matchId2 = sut.StartNew(new Team(CountryEnum.Mexico), new Team(CountryEnum.Canada));

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

        var matchId = sut.StartNew(new Team(CountryEnum.Mexico), new Team(CountryEnum.Canada));

        sut.UpdateScore(matchId, 0, 5);

        var match = sut.GetInprogressMatches().First();

        match.HomeTeamScore.Should().Be(0);
        match.AwayTeamScore.Should().Be(5);
    }

    [Fact]
    public void RemoveNonExistingMatchTest()
    {
        var sut = new Scoreboard();

        sut.StartNew(new Team(CountryEnum.Mexico), new Team(CountryEnum.Canada));

        sut.RemoveMatch(Guid.NewGuid());

        var matches = sut.GetInprogressMatches();

        matches.Should().HaveCount(1);
    }

    [Fact]
    public void UpdateNonExistingMatchTest()
    {
        var sut = new Scoreboard();

        sut.StartNew(new Team(CountryEnum.Mexico), new Team(CountryEnum.Canada));

        sut.UpdateScore(Guid.NewGuid(), 20, 1);

        var matches = sut.GetInprogressMatches();

        matches.Should().HaveCount(1);
    }
}