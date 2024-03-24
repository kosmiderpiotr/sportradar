namespace FootballWorldCupScoreboard;

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