//using Foundation;
using Microsoft.Maui.Controls;
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
        readonly string apiKey = "25a76397ce707ce354744acbafd9acf5";
        readonly string apiUrl = "https://api.the-odds-api.com/";
        readonly string regions = "eu";
        readonly string markets = "h2h";

        public async Task<List<SportsApiData>> GetApiDataAsync(string sportName)
        {

            var url = $"{apiUrl}/v4/sports/{sportName}/odds/?apiKey={apiKey}&regions={regions}&markets={markets}";
            List<SportsApiData> sportsApiData = await ReadApiasync(url);

            return sportsApiData;
        }

        public async Task<List<SportsApiData>> ReadApiasync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var sportsApiData = await response.Content.ReadFromJsonAsync<List<SportsApiData>>();

                return sportsApiData ?? new List<SportsApiData>();

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
    }
}
