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

    public void UpdateScore(Guid matchId, int homeTeamScore, int awayTeamScore)
    {
        var oldMatch = _matches[matchId];

        _matches[matchId] = oldMatch with { HomeTeamScore = homeTeamScore, AwayTeamScore = awayTeamScore };
    }

    public List<Match> GetInprogressMatches()
    {
        return _matches.Values
            .OrderByDescending(x => x.TotalScore)
            .ThenByDescending(x => x.StartDate)
            .ToList();
    }
}