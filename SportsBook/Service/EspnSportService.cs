//using Org.Apache.Http.Conn;
using SportsBook.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace SportsBook.Service
{
    public class EspnSportService
    {
        private readonly HttpClient _httpClient;
        private readonly object _locker = new object();

        public EspnSportService()
        {
            _httpClient = new HttpClient();
        }

        //Step one get sport and sportname to list
        public async Task<List<EspnSportsName>> GetEspnSportsAsync()
        {
            string apiUrl = "https://sports.core.api.espn.com/v2/sports?lang=en&region=us";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to fetch sports data: {response.StatusCode}");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            using JsonDocument doc = JsonDocument.Parse(jsonResponse);

            List<EspnSportsName> sportsList = new List<EspnSportsName>();

            if (doc.RootElement.TryGetProperty("items", out JsonElement items))
            {
                foreach (JsonElement item in items.EnumerateArray())
                {
                    if (item.TryGetProperty("$ref", out JsonElement refElement))
                    {
                        string url = refElement.GetString();
                        string name = url.Split("/sports/")[1].Split('?')[0];// Extracts "australian-football", "baseball", etc.

                        sportsList.Add(new EspnSportsName
                        {
                            SportName = name.Replace("-", " ").ToLower(), // Formatting name
                            SportUrl = url
                        });
                    }
                }
            }
            return sportsList;
        }

        //step two sport info
        public async Task<EspnSport> EspnFetchSportData(string url)
        {
            using HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(url);

            // Deserialize the JSON into the Sport object
            EspnSport sport = JsonSerializer.Deserialize<EspnSport>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            sport.LeagueNames = await FetchLeaguesWithNames(sport.Leagues.Ref, sport.Slug); // lägger till league names and link to sport

            return sport;
        }


        //step 3 league names, sport and link
        public static async Task<List<EspnLeagueGroupNameAndRef>> FetchLeaguesWithNames(string leaguesApiUrl, string sportSlug)
        {
            using HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(leaguesApiUrl);

            // Deserialize JSON into League List
            var leagueList = JsonSerializer.Deserialize<EspnLeagueListResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            List<EspnLeagueGroupNameAndRef> leagues = new List<EspnLeagueGroupNameAndRef>();

            // Fetch details for each league
            foreach (var league in leagueList.Items)
            {
                try
                {
                    string leagueJson = await client.GetStringAsync(league.Url);
                    var leagueDetails = JsonSerializer.Deserialize<EspnLeagueInfo>(leagueJson,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    leagues.Add(new EspnLeagueGroupNameAndRef() { name = leagueDetails.DisplayName, Url = league.Url, SportSlug = sportSlug, LeagueSlug = leagueDetails.Slug });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to fetch: {league.Url} - {ex.Message}");
                }
            }

            return leagues;
        }


        //step 4 league info 
        public async Task<EspnLeagueInfo> EspnFetchLeagueDetails(string leagueUrl)
        {
            using HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(leagueUrl);

            var leagueInfo = JsonSerializer.Deserialize<EspnLeagueInfo>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return leagueInfo;
        }

        //step fem League Games
        public async Task<EspnSportLeagueData> EspnLeagueGamesData(string sportSlug, string league)
        {
            using HttpClient _httpClient = new HttpClient();

            string date = "20250215";
            var url = $"https://site.api.espn.com/apis/site/v2/sports/{sportSlug}/{league}/scoreboard";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var sportsApiData = await response.Content.ReadFromJsonAsync<EspnSportLeagueData>();

                return sportsApiData ?? new EspnSportLeagueData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception();
            }


        }

        
        public async Task<List<EspnSportLeagueData>> EspnLeagueGamesDataForMoreThenOneDay(string sportSlug, string league)
        {
            DateTime today = DateTime.UtcNow;
            int dayOfWeek = (int)today.DayOfWeek - 1;
            if (dayOfWeek < 0) dayOfWeek = 6;

            DateTime startOfWeek = today.AddDays(-dayOfWeek);
            List<Task<EspnSportLeagueData>> tasks = new List<Task<EspnSportLeagueData>>();

            try
            {
                for (int i = 0; i < 7; i++)
                {
                    string date = startOfWeek.AddDays(i).ToString("yyyyMMdd");
                    var url = $"https://site.api.espn.com/apis/site/v2/sports/{sportSlug}/{league}/scoreboard?dates={date}";
                    tasks.Add(FetchGameData(url));
                }

                var results = await Task.WhenAll(tasks);
                return results.Where(r => r != null).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return new List<EspnSportLeagueData>();
            }
        }

        private async Task<EspnSportLeagueData> FetchGameData(string url)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10)); // 10s timeout
            HttpResponseMessage response = await _httpClient.GetAsync(url, cts.Token);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EspnSportLeagueData>();
        }



        //step six game stats

        public async Task<EspnGameStatsData> GetGameStats(string gameId, string sport, string league)
        {
            using HttpClient _httpClient = new HttpClient();

            var url = $"http://site.api.espn.com/apis/site/v2/sports/{sport}/{league}/summary?event={gameId}";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var gameStatsData = await response.Content.ReadFromJsonAsync<EspnGameStatsData>();

                return gameStatsData ?? new EspnGameStatsData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
                throw new Exception();
            }
        }

    }



}

