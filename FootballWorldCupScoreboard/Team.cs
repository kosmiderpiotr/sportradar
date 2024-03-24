using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballWorldCupScoreboard
{
    public class Team(CountryEnum country)
    {
        public CountryEnum Country { get; } = country;
    };
}
