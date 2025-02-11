using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBook.Models
{
    public class EspnGameData
    {
        public string? gameId { get; set; }
        public string? homeTeam { get; set; }
        public string? homeTeamLogo { get; set; }
        public string? homeTeamScore { get; set; }
        public double? homeTeamOdds { get; set; }

        public double? drawOdds { get; set; }

        public string? awayTeam { get; set; }
        public string? awayTeamScore { get; set;}
        public string? awayTeamLogo { get; set; }
        public double? awayTeamOdds { get; set; }




        public string? displayClock { get; set; }
        public DateTime? date { get; set; }
        public string? gameStatus { get; set; }


    }

    public class EspnGameStats
    {
        public string? foulsCommitted { get; set; }
        public string? wonCorners {  get; set; }
        public string? possessionPct { get; set; }
        public string? totalShots { get; set; }
        public string? shotsOnTarget { get; set; }

        public ObservableCollection<WhatHappend>? whatHappend { get; set; }

        public int? yellowCard {  get; set; }
        public int? redCard { get; set; }


    }

    public class WhatHappend
    {
        public string? time { get; set; }
        public string? type { get; set; }
        public string? athletesInvolved { get; set; }

    }
}
