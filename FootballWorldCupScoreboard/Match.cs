namespace FootballWorldCupScoreboard;

public class Match
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime StartDate { get; init; } = DateTime.Now;

    public Team HomeTeam { get; }

    public Team AwayTeam { get; }

    public int HomeTeamScore { get; set; } = 0;

    public int AwayTeamScore { get; set; } = 0;

    public int TotalScore => HomeTeamScore + AwayTeamScore;

    public Match(Team homeTeam, Team awayTeam)
    {
        HomeTeam = homeTeam;

        AwayTeam = awayTeam;
    }
}