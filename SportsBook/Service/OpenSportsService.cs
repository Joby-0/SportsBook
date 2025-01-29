//using Foundation;
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
        readonly string apiKey = "214fd128fb2e1b884caa6b784285de73";


        readonly string apiUrl = "https://api.the-odds-api.com/";
        readonly string regions = "eu";
        readonly string markets = "h2h";
        readonly string daysFrom = "1"
        readonly string dateFormat = "iso"

        public async Task<List<SportsApiData>> GetApiDataAsyncOdds(string sportName)
        {

            var url = $"{apiUrl}/v4/sports/{sportName}/odds/?apiKey={apiKey}&regions={regions}&markets={markets}";
            List<SportsApiData> sportsApiData = await ReadApiasyncOdds(url);

            return sportsApiData;
        }

        public async Task<List<SportsApiData>> ReadApiasyncOdds(string url)
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

        public async Task<List<SportsApiData>> GetApiDataAsyncScores(string sportName)
        {

            var url = $"/v4/sports/{sportName}/scores/?apiKey={apiKey}&daysFrom={daysFrom}&dateFormat={dateFormat}";
            List<SportsApiData> sportsApiData = await ReadApiasyncScores(url);

            return sportsApiData;
        }

        public async Task<List<SportsApiData>> ReadApiasyncScores(string url)
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
    }
}
