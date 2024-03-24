namespace FootballWorldCupScoreboard;

public class Scoreboard
{
    private Dictionary<Guid, Match> _matches { get; init; } = [];

    public Guid StartNew(Team homeTeam, Team awayTeam)
    {
        var newMath = new Match(homeTeam, awayTeam, 0, 0);

        _matches.Add(newMath.Id, newMath);

        return newMath.Id;
    }

    public void RemoveMatch(Guid matchId)
    {
        _matches.Remove(matchId);
    }

    public bool UpdateScore(Guid matchId, int homeTeamScore, int awayTeamScore)
    {
        if (homeTeamScore < 0)
            throw new ArgumentException("Home team score can't be negative");

        if (awayTeamScore < 0)
            throw new ArgumentException("Away team score can't be negative");

        if (!_matches.ContainsKey(matchId))
            return false;

        var oldMatch = _matches[matchId];

        _matches[matchId] = oldMatch with { HomeTeamScore = homeTeamScore, AwayTeamScore = awayTeamScore };

        return true;
    }

    public List<Match> GetInprogressMatches()
    {
        return _matches.Values
            .OrderByDescending(x => x.TotalScore)
            .ThenByDescending(x => x.StartDate)
            .ToList();
    }
}