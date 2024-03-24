namespace FootballWorldCupScoreboard
{
    public sealed class Team(CountryEnum country)
    {
        public CountryEnum Country { get; } = country;
    };
}
