namespace FootballWorldCupScoreboard;


public record Match(Team HomeTeam, Team AwayTeam, int HomeTeamScore, int AwayTeamScore)
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime StartDate { get; } = DateTime.Now;

    public int TotalScore => HomeTeamScore + AwayTeamScore;

}