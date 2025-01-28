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

        public SportsPage(string sport, IEnumerable<League> sports)
        {
            InitializeComponent();
            sportname = sport;
            service = new OpenSportsService();
            
            //this.leagueList = sports ?? new List<League>(); //vill bara få den man klcikar på
            leagueList = new List<League>(sports ?? new List<League>());
            

            LoadLeagueDataAsync();


        }
        private async void LoadLeagueDataAsync()
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
                        league.LeagueGames = await service.GetApiDataAsync(league.Key);
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
            var selectedLeague = e.CurrentSelection as League; //blir null

            if(selectedLeague != null)
            {
                //LeagueGamesListView.ItemsSource = selectedLeague.LeagueGames;


                await DisplayAlert("hej", $"{selectedLeague.Title}", "OK");


            }
        }

        
    }

}
