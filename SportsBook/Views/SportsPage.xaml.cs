using SportsBook.Models;
using SportsBook.Service;

namespace SportsBook
{

    public partial class SportsPage : ContentPage
    {

        OpenSportsService service;
        IEnumerable<League> leagueList;
        string sportname;

        public SportsPage(string sport, IEnumerable<League> sports)
        {
            InitializeComponent();
            this.sportname = sport;
            service = new OpenSportsService();
            this.leagueList = sports ?? new List<League>(); //vill bara få den man klcikar på
            LoadLeagueDataAsync();

        }
        private async void LoadLeagueDataAsync()
        {
            try
            {
                foreach (var league in this.leagueList)
                {
                    if (league.Key.ToLower().Contains("winner"))
                    {
                        continue;
                    }
                    else
                    {
                        league.LeagueGames = await service.GetApiDataAsync(league.Key);
                        //foreach (var item in this.sportList)
                        //{
                        //    tabLeague.Title = item.Title;
                        //    tabLeague.Items.Add(new ShellContent());
                        //}

                    }
                }



            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load data: {ex.Message}", "OK");
            }
        }
    }

}
