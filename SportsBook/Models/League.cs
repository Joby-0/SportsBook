using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBook.Models
{
    public class GroupedSports
    {
        public string sportname { get; set; }
        //public IEnumerable<IGrouping<string, List<Sports>>> Groupedlist { get; set; }
        public List<League> Sportslist { get; set; }
    }
    public class Sport
    {
        public string SportsName { get; set; }

        public List<League> List { get; set; } = new List<League>();

    }
    public class League
    {
        public string Key { get; set; }
        public string Group { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool HasOutrights { get; set; }

        public List<SportsApiData>? LeagueGames { get; set; } // sätt in matcherna i den här 

        
    }
}
