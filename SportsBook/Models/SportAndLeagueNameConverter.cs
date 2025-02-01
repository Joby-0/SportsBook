using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBook.Models
{
    public class SportAndLeagueNameConverter
    {
        private static readonly Dictionary<string, string> LeagueNames = new()
        {
                { "soccer_uefa_europa_league", "uefa.europa" },
                { "soccer_uefa_europa_conference_league", "uefa.europa.conf" },
                { "soccer_uefa_champs_league", "uefa.champions" },
                { "soccer_turkey_super_league", "tur.1" },
                { "soccer_switzerland_superleague", "sui.1" },
                { "soccer_sweden_allsvenskan", "swe.1" },
                { "soccer_spl", "sco.1" },
                { "soccer_spain_segunda_division", "esp.2" },
                { "soccer_spain_la_liga", "esp.1" },
                { "soccer_portugal_primeira_liga", "por.1" },
                //{ "soccer_poland_ekstraklasa", "" },
                { "soccer_norway_eliteserien", "nor.1" },
                { "soccer_netherlands_eredivisie", "ned.1" },
                { "soccer_mexico_ligamx", "mex.1" },
                { "soccer_league_of_ireland", "irl.1" },
                //{ "soccer_korea_kleague1", "" },
                { "soccer_japan_j_league", "jpn.1" },
                { "soccer_italy_serie_b", "ita.2" },
                { "soccer_italy_serie_a", "ita.1" },
                { "soccer_greece_super_league", "gre.1" },
                { "soccer_germany_liga3", "ger.3" },
                { "soccer_germany_bundesliga2", "ger.2" },
                { "soccer_germany_bundesliga", "ger.1" },
                { "soccer_france_ligue_two", "fra.2" },
                { "soccer_france_ligue_one", "fra.1" },
                //{ "soccer_fifa_world_cup_winner", "" },
                { "soccer_fa_cup", "eng.fa" },
                { "soccer_epl", "eng.1" },
                { "soccer_england_league2", "eng.4" },
                { "soccer_england_league1", "eng.3" },
                { "soccer_england_efl_cup", "eng.league_cup" },
                { "soccer_efl_champ", "eng.2" },
                { "soccer_denmark_superliga", "den.1" },
                { "soccer_conmebol_copa_libertadores", "conmebol.libertadores" },
                { "soccer_belgium_first_div", "bel.1" },
                { "soccer_austria_bundesliga", "aut.1" },
                { "soccer_australia_aleague", "aus.1" },
                { "soccer_argentina_primera_division", "arg.1" },

                // Rugby
                { "rugbyunion_six_nations", "180659" },
                //{ "rugbyleague_nrl", "" },

                // MMA
                { "mma_mixed_martial_arts", "" },

                // Ice Hockey
                { "icehockey_sweden_hockey_league", "" },
                { "icehockey_sweden_allsvenskan", "" },
                //{ "icehockey_nhl_championship_winner", "" },
                { "icehockey_nhl", "nhl" },

                // Golf
                { "golf_us_open_winner", "" },
                { "golf_the_open_championship_winner", "" },
                { "golf_pga_championship_winner", "" },
                { "golf_masters_tournament_winner", "" },

                // Cricket
                { "cricket_test_match", "" },
                { "cricket_odi", "" },
                { "cricket_international_t20", "" },

                // Boxing
                { "boxing_boxing", "" },

                // Basketball
                { "basketball_ncaab_championship_winner", "" },
                { "basketball_ncaab", "mens-college-basketball" },
                { "basketball_nbl", "nbl" },
                { "basketball_nba_championship_winner", "" },
                { "basketball_nba", "nba" },
                { "basketball_euroleague", "" },

                // Baseball
                { "baseball_mlb_world_series_winner", "" },
                { "baseball_mlb_world_series", "mlb" },

                // Aussie Rules
                { "aussierules_afl", "" },

                // American Football
                //{ "americanfootball_nfl_super_bowl_winner", "" },
                { "americanfootball_nfl", "nfl" },
                { "americanfootball_ncaaf", "college-football" }



        };

        private static readonly Dictionary<string, string> SportNames = new()
        {
            {"American Football","football" },
            {"Baseball", "baseball" },
            {"Cricket", "cricket" },
            {"Golf", "golf" },
            {"Ice Hockey", "hockey" },
            {"Rugby League", "rugby" },
            {"Soccer", "soccer" },
            {"Tennis", "tennis" }
        };

        public string LeagueNamesConvert(string LeagueName)
        {
            var newLeagueName = "";

            if(LeagueNames.TryGetValue(LeagueName, out string? retunstring))
            {
                if(!string.IsNullOrEmpty(retunstring)) newLeagueName = retunstring;
            }



            return newLeagueName;
        }
        public string SportNamesConvert(string LeagueName)
        {
            var newSportName = "";

            if (SportNames.TryGetValue(LeagueName, out string? retunstring))
            {
                if (!string.IsNullOrEmpty(retunstring)) newSportName = retunstring;
            }



            return newSportName;
        }
    }
}
