﻿//using Foundation;
//using ABI.System;
using Microsoft.Maui.Controls;
using Microsoft.VisualBasic;
using SportsBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
//using UIKit;

namespace SportsBook.Service
{
    internal class OpenSportsService
    {
        readonly HttpClient _httpClient = new HttpClient();
        //readonly string apiKey = "25a76397ce707ce354744acbafd9acf5";
        //readonly string apiKey = "773f1bb5185087c5a40801f8fa5c5eb6";
        //readonly string apiKey = "214fd128fb2e1b884caa6b784285de73";
        //readonly string apiKey = "0bc05f854dc6a615cdefa490da07a30f";
        readonly string apiKey = "e3481e2ebe88d5098c5264eb2757a5d4";






        readonly string apiUrl = "https://api.the-odds-api.com/";
        readonly string regions = "eu";
        readonly string markets = "h2h";
        readonly string daysFrom = "3";
        readonly string dateFormat = "iso";



        public async Task<List<SportsApiDataOdds>> GetApiDataAsyncOdds(string sportName)
        {

            var url = $"{apiUrl}/v4/sports/{sportName}/odds/?apiKey={apiKey}&regions={regions}&markets={markets}";
            List<SportsApiDataOdds> sportsApiData = await ReadApiasyncOdds(url);

            return sportsApiData;
        }

        public async Task<List<SportsApiDataOdds>> ReadApiasyncOdds(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var sportsApiData = await response.Content.ReadFromJsonAsync<List<SportsApiDataOdds>>();

                return sportsApiData ?? new List<SportsApiDataOdds>();

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<League>> GetApiSportsAsync()
        {

            var url = $"{apiUrl}v4/sports?apiKey={apiKey}";
            List<League> sportsname = await GetSportsAsync(url);

            return sportsname;
        }
        public async Task<List<League>> GetSportsAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var sportsname = await response.Content.ReadFromJsonAsync<List<League>>();


                return sportsname ?? new List<League>();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<SportApiDataScores>> GetApiDataAsyncScores(string sportName)
        {

            var url = $"{apiUrl}v4/sports/{sportName}/scores/?apiKey={apiKey}&daysFrom={daysFrom}&dateFormat={dateFormat}";
            List<SportApiDataScores> sportsApiData = await ReadApiasyncScores(url);

            return sportsApiData;
        }

        public async Task<List<SportApiDataScores>> ReadApiasyncScores(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var sportsApiData = await response.Content.ReadFromJsonAsync<List<SportApiDataScores>>();

                return sportsApiData ?? new List<SportApiDataScores>();

            }
            catch (Exception ex)
            {

                throw;
            }

        }



        //https://site.web.api.espn.com/apis/site/v2/sports/{soccer}/{eng.1}/teams/

        readonly string logoApiUrl = "https://site.web.api.espn.com";


        //public async Task GetLeagueAndAllTeamLogos(League league)
        //{
        //    List<GroupedTeamsAndLogo> groupedTeamsList = new List<GroupedTeamsAndLogo>();
        //    var nameConverter = new SportAndLeagueNameConverter();

        //    string? sport = league.Group;
        //    string? leagueKey = league.Key;

        //    string sport2 = "soccer";
        //    string league2 = "eng.1";
        //    if (sport != null && leagueKey != null)
        //    {
        //        string leagueName = nameConverter.LeagueNamesConvert(leagueKey);
        //        string sportName = nameConverter.SportNamesConvert(sport);


        //        var url = $"{logoApiUrl}/apis/site/v2/sports/{sportName.ToLower()}/{leagueName}/teams";
        //        SportApiDataLeagueTeam LeagueTeams = await GetTeamLogoApi(url);

        //        if (LeagueTeams?.sports != null)
        //        {
        //            foreach (var _sport in LeagueTeams.sports)
        //            {
        //                if (_sport?.leagues != null)
        //                {
        //                    foreach (var _league in _sport.leagues)
        //                    {
        //                        var groupedTeams = new GroupedTeamsAndLogo
        //                        {
        //                            LeagueName = _league.name,
        //                            Teams = new Dictionary<string, string>()
        //                        };

        //                        if (_league?.teams != null)
        //                        {
        //                            foreach (var teamWrapper in _league.teams)
        //                            {
        //                                var team = teamWrapper.team;
        //                                if (team != null && team.logos != null && team.logos.Count > 0)
        //                                {
        //                                    string logoUrl = team.logos.First().href; // Get the first logo
        //                                    groupedTeams.Teams[team.displayName] = logoUrl;
        //                                }
        //                            }
        //                        }

        //                        groupedTeamsList.Add(groupedTeams);
        //                        AddTeamLogoToMatch(league, groupedTeams);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //public async Task<SportApiDataLeagueTeam> GetTeamLogoApi(string url)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await _httpClient.GetAsync(url);
        //        response.EnsureSuccessStatusCode();
        //        var sportsApiDataTeam = await response.Content.ReadFromJsonAsync<SportApiDataLeagueTeam>();

        //        return sportsApiDataTeam;

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public async void AddTeamLogoToMatch(League league, GroupedTeamsAndLogo teamsAndLogo)
        //{
        //    if (league.LeagueGames != null)
        //    {

        //        foreach (var game in league.LeagueGames)
        //        {
        //            if (teamsAndLogo.Teams.TryGetValue(game.home_team, out string? homeLogo))
        //            {
        //                game.home_team_logo = homeLogo;
        //            }

        //            if (teamsAndLogo.Teams.TryGetValue(game.away_team, out string? awayLogo))
        //            {
        //                game.away_team_logo = awayLogo;
        //            }
        //        }
        //    }

        //}
    }
}
