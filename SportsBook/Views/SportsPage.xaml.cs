﻿using SportsBook.Models;
using SportsBook.Service;

using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml;
using System.Text.Json;
using SportsBook.ViewModels;
using SportsBook.Views;


namespace SportsBook
{

    public partial class SportsPage : ContentPage
    {

        private readonly SportsPageViewModel _viewModel;

        public SportsPage(string sportName, string sportUrl, EspnSportService espnSportService)
        {
            InitializeComponent();
            _viewModel = new SportsPageViewModel(espnSportService);
            BindingContext = _viewModel;
            LoadData(sportUrl);
        }

        private async void LoadData(string sportUrl)
        {
            await _viewModel.LoadLeagueGamesAndScoresDataAsync(sportUrl);
        }

        // Trigger when a league is selected
        private async void LeagueTabList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var selectedLeague = e.CurrentSelection.FirstOrDefault() as EspnLeagueGroupNameAndRef;
                if (selectedLeague != null)
                {
                    await _viewModel.GameViewModel.FetchAndUpdateGames(selectedLeague, selectedLeague.LeagueSlug);
                }
            }
        }

        //tap on a game
        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var gameData = (sender as BindableObject)?.BindingContext as EspnGameData;
            if (gameData != null)
            {
                //await Navigation.PushModalAsync(new GameStatsPage(gameData));
                await Shell.Current.Navigation.PushAsync(new GameStatsPage(gameData));
                //await Shell.Current.GoToAsync(new GameStatsPage(gameData));
            }


        }

        //private async void LoadLeagueGamesAndScoresDataAsync(string sportUrl)
        //{
        //    try
        //    {
        //        var espnLeague = await EspnSportService.EspnFetchSportData(sportUrl);
        //        EspnLeagueList = espnLeague.LeagueNames.ToList();
        //        LeagueTabList.ItemsSource = EspnLeagueList;


        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Error", $"Failed to load data: {ex.Message}", "OK");
        //    }
        //}


        //private async void LeagueTabList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        //    {
        //        var selectedLeague = e.CurrentSelection.FirstOrDefault() as EspnLeagueGroupNameAndRef;

        //        if (selectedLeague != null)
        //        {
        //            try
        //            {
        //                var leagueInfo = await EspnSportService.EspnFetchLeagueDetails(selectedLeague.Url);
        //                //await EspnSportService.EspnLeagueGamesDataForSevenDays(selectedLeague.SportSlug, leagueInfo.Slug);
        //                var matchinfo = await EspnSportService.EspnLeagueGamesData(selectedLeague.SportSlug, leagueInfo.Slug);


        //                if (matchinfo?.events != null && matchinfo.events.Any())
        //                {
        //                    var matchList = matchinfo.events.Select(eventData =>
        //                    {
        //                        try
        //                        {
        //                            var competition = eventData.competitions.FirstOrDefault();
        //                            if (competition == null || competition.competitors.Count < 0)
        //                                return null;

        //                            return new EspnGameData
        //                            {
        //                                homeTeam = competition.competitors[0]?.team?.name ?? "Unknown",
        //                                awayTeam = competition.competitors[1]?.team?.name ?? "Unknown",
        //                                homeTeamLogo = competition.competitors[0]?.team?.logo,
        //                                awayTeamLogo = competition.competitors[1]?.team?.logo,
        //                                homeTeamScore = competition.competitors[0]?.score ?? "0",
        //                                awayTeamScore = competition.competitors[1]?.score ?? "0",
        //                                date = competition.date.Value,
        //                                displayClock = eventData.competitions.Select(a => a.status.displayClock).Take(1).FirstOrDefault()
        //                                //clock = competition.status?.clock as double? ?? 0.0

        //                            };
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            Debug.WriteLine($"Error processing match: {ex.Message}");
        //                            return null;
        //                        }

        //                    }).Where(match => match != null).ToList();

        //                    // Ensure UI update happens on the main thread
        //                    MainThread.BeginInvokeOnMainThread(() =>
        //                    {
        //                        // Code to run on the main thread
        //                        GamesFilterMenu.IsVisible = true;
        //                        LeagueGamesGrid.ItemsSource = matchList;

        //                    });


        //                }
        //                else
        //                {
        //                    await DisplayAlert("Notice", "No games found", "OK");
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Debug.WriteLine($"Error fetching league games: {ex.Message}");
        //                await DisplayAlert("Error", "Failed to load league games.", "OK");
        //            }
        //        }
        //    }
        //}

    }
}
