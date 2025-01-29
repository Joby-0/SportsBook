using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBook.Models
{
    public class SportApiDataScores
    {
        public string? id { get; set; }
        public string? sport_key { get; set; }
        public string? sport_title { get; set; }
        public DateTime commence_time { get; set; }
        public bool completed { get; set; }
        public string? home_team { get; set; }
        public string? away_team { get; set; }
        public List<Scores>? scores { get; set; }
        
        public DateTime? last_update { get; set; }
    }

    public class Scores
    {
        public string? name { get; set; }
        public string? score { get; set; }
    }
}
