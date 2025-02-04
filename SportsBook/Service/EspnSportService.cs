using SportsBook.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace SportsBook.Service
{
    internal class EspnSportService
    {
        private readonly HttpClient _httpClient;

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
        public static async Task<EspnSport> EspnFetchSportData(string url)
        {
            using HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(url);

            // Deserialize the JSON into the Sport object
            EspnSport sport = JsonSerializer.Deserialize<EspnSport>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            sport.LeagueNames = await FetchLeaguesWithNames(sport.Leagues.Ref, sport.Slug); // lägger till league names and link to sport
            
            return sport;
        }



        //step 3 league names
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
                    var leagueDetails = JsonSerializer.Deserialize<EspnLeagueDetails>(leagueJson,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    leagues.Add(new EspnLeagueGroupNameAndRef() { name = leagueDetails.Name, Url = league.Url, SportSlug = sportSlug});
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to fetch: {league.Url} - {ex.Message}");
                }
            }
            
            return leagues;
        }


        //step 4 league info 
        public static async Task<EspnLeagueInfo> EspnFetchLeagueDetails(string leagueUrl)
        {
            using HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(leagueUrl);

            var leagueInfo =  JsonSerializer.Deserialize<EspnLeagueInfo>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return leagueInfo;
        }

        //step fem League Games
        //public static async Task<EspnSportLeagueData> EspnLeagueGamesData(string sportSlug, string league)
        //{
        //    using HttpClient client = new HttpClient();
        //    var url = $"https://site.api.espn.com/apis/site/v2/sports/{sportSlug}/{league}/scoreboard";
        //    string json = await client.GetStringAsync(url);

        //    var leagueGamesInfo = JsonSerializer.Deserialize<EspnSportLeagueData>(json,
        //        new JsonSerializerOptions { PropertyNameCaseInsensitive = false });

        //    return leagueGamesInfo;
        //}
        Exception Ex;
        public async Task<EspnSportLeagueData> EspnLeagueGamesData(string sportSlug, string league)
        {
            using HttpClient _httpClient = new HttpClient();
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
                ex = Ex;
                throw new Exception();
            }


        }



    }



}

