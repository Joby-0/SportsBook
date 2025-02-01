using SportsBook.Models;
using SportsBook.Service;

using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;

namespace SportsBook
{

    public partial class SportsPage : ContentPage
    {

        OpenSportsService service;
        IEnumerable<League> leagueList;
        string sportname;
        GroupedTeamsAndLogo groupedTeamsAndLogo;

        public SportsPage(string sport, IEnumerable<League> sports)
        {
            InitializeComponent();
            sportname = sport;
            service = new OpenSportsService();
            
            //this.leagueList = sports ?? new List<League>(); //vill bara få den man klcikar på
            leagueList = new List<League>(sports ?? new List<League>());
            

            LoadLeagueGamesAndScoresDataAsync();


        }
        private async void LoadLeagueGamesAndScoresDataAsync()
        {
            try
            {
                
                foreach (var league in leagueList)
                {
                    if (league.Key.ToLower().Contains("winner"))
                    {
                        continue;
                    }
                    else
                    {
                        //league.LeagueGames = await service.GetApiDataAsyncOdds(league.Key);
                        league.LeagueGames = await service.GetApiDataAsyncScores(league.Key);
                        LeagueTabList.ItemsSource = leagueList;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load data: {ex.Message}", "OK");
            }
        }

        private async void LeagueTabList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var selectedLeague = e.CurrentSelection.FirstOrDefault() as League; 

                if (selectedLeague != null)
                {
                    //LeagueGamesListView.ItemsSource = selectedLeague.LeagueGames;
                    await LoadLeagueTeamsLogo(selectedLeague);

                    LeagueGamesGrid.ItemsSource = selectedLeague.LeagueGames;

                    


                }
            }
            else 
            { 
                    await DisplayAlert("hej", $"No games found", "OK");

            }

        }
        
        private async Task LoadLeagueTeamsLogo(League league)
        {
            
            if (league != null )
            {
                await service.GetLeagueAndAllTeamLogos(league);
            }



        }



    }

}
