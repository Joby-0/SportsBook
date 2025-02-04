using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBook.Models
{
    public class EspnGameData
    {
        public string? homeTeam { get; set; }
        public string? awayTeam { get; set; }
        public string? homeTeamLogo { get; set; }
        public string? awayTeamLogo { get; set; }
        public DateTime? date { get; set; }

        public string? homeTeamScore { get; set; }
        public string? awayTeamScore { get; set;}

        public double? clock {  get; set; }

    }
}
