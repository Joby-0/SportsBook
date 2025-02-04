using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBook.Models
{
    
    public class SportsApiDataOdds
    {
        public string? id { get; set; }
        public string? sportKey { get; set; }
        public DateTime? commence_time { get; set; }
        public string? home_team { get; set; }
        public string? away_team { get; set; }
        public List<Bookmaker>? bookmakers { get; set; }

        
    }
    public class Bookmaker
    {
        public string? key { get; set; }
        public string? title { get; set; }
        public DateTime? lastUpdate { get; set; }
        public List<Market>? markets { get; set; }
    }

    public class Market
    {
        public string? key { get; set; }
        public List<Outcome>? outcomes { get; set; }
    }

    public class Outcome
    {
        public string? name { get; set; }
        public decimal? price { get; set; }
        public decimal? point { get; set; } // Nullable, as not all outcomes have a point value
    }

}
