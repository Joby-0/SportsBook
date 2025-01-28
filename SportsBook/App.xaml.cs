using SportsBook.Models;

namespace SportsBook
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var sport = new { sportname = "Football" };
            var listOfLeagues = new List<League>
            {
                new League { Description = "Premier League", Key = "soccer" },
                new League { Description = "La Liga", Key = "basketball_nba" }
            };
            //MainPage = new TappedMeny(sport.sportname, listOfLeagues);
            MainPage = new AppShell();
        }

        
    }
}
