using SportsBook.Models;
using SportsBook.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SportsBook.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private readonly EspnSportService _espnSportService = new EspnSportService();


        private ObservableCollection<EspnGameData> _matchList;
        public ObservableCollection<EspnGameData> MatchList
        {
            get => _matchList;
            set
            {
                if (_matchList != value)
                {
                    _matchList = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isGamesVisible;
        public bool IsGamesVisible
        {
            get => _isGamesVisible;
            set
            {
                if (_isGamesVisible != value)
                {
                    _isGamesVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        private CancellationTokenSource _cancellationTokenSource;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task FetchAndUpdateGames(EspnLeagueGroupNameAndRef selectedLeague, string leagueSlug)
        {
            try
            {
                var leagueInfo = await _espnSportService.EspnFetchLeagueDetails(selectedLeague.Url);
                var matchInfo = await _espnSportService.EspnLeagueGamesData(selectedLeague.SportSlug, leagueInfo.Slug);

                if (matchInfo?.events != null && matchInfo.events.Any())
                {
                    MatchList = new ObservableCollection<EspnGameData>(
                        matchInfo.events.Select(eventData =>
                        {
                            try
                            {
                                var competition = eventData.competitions.FirstOrDefault();
                                if (competition == null || competition.competitors.Count < 0)
                                    return null;

                                return new EspnGameData
                                {
                                    gameId = eventData.id,
                                    homeTeam = competition.competitors[0]?.team?.name ?? "Unknown",
                                    awayTeam = competition.competitors[1]?.team?.name ?? "Unknown",
                                    homeTeamLogo = competition.competitors[0]?.team?.logo,
                                    awayTeamLogo = competition.competitors[1]?.team?.logo,
                                    homeTeamScore = competition.competitors[0]?.score ?? "0",
                                    awayTeamScore = competition.competitors[1]?.score ?? "0",
                                    //awayTeamOdds = competition.odds[0]?.awayTeamOdds.value ?? 0,
                                    //homeTeamOdds = competition.odds[0]?.homeTeamOdds?.value ?? 0,
                                    //drawOdds = competition.odds[0]?.drawOdds?.value ?? 0,
                                    date = competition.date.Value,
                                    displayClock = eventData.competitions.Select(a => a.status.displayClock).Take(1).FirstOrDefault()
                                };
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Error processing match: {ex.Message}");
                                return null;
                            }
                        }).Where(match => match != null)
                    );

                    IsGamesVisible = true;
                    StartPeriodicUpdate(selectedLeague, leagueInfo.Slug);
                }
                else
                {
                    IsGamesVisible = false;
                    // Display a message that no games are found
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching league games: {ex.Message}");
                // Display an error message
            }
        }
        private void StartPeriodicUpdate(EspnLeagueGroupNameAndRef selectedLeague, string leagueSlug)
        {
            // Cancel any previous updates
            _cancellationTokenSource?.Cancel();

            // Set a new cancellation token source
            _cancellationTokenSource = new CancellationTokenSource();

            // Set a timer to refresh the game stats every 30 seconds
            StartUpdatingGameStats(selectedLeague, leagueSlug);
        }
        public void StartUpdatingGameStats(EspnLeagueGroupNameAndRef selectedLeague, string leagueSlug)
        {
            // This will start the background task that updates the game stats every 30 seconds
            Task.Run(async () => await StartGameStatsUpdater(selectedLeague, leagueSlug));
        }

        public async Task StartGameStatsUpdater(EspnLeagueGroupNameAndRef selectedLeague, string leagueSlug)
        {
            while (true)
            {
                await UpdateGameStats(selectedLeague, leagueSlug);  // Your method to update the game stats
                await Task.Delay(TimeSpan.FromSeconds(30));  // Wait for 30 seconds before the next update
            }
        }

        private async Task UpdateGameStats(EspnLeagueGroupNameAndRef selectedLeague, string leagueSlug)
        {
            try
            {
                var matchInfo = await _espnSportService.EspnLeagueGamesData(selectedLeague.SportSlug, leagueSlug);

                if (matchInfo?.events != null && matchInfo.events.Any())
                {
                    MatchList = new ObservableCollection<EspnGameData>(
                        matchInfo.events.Select(eventData =>
                        {
                            try
                            {
                                var competition = eventData.competitions.FirstOrDefault();
                                if (competition == null || competition.competitors.Count < 0)
                                    return null;

                                return new EspnGameData
                                {
                                    homeTeam = competition.competitors[0]?.team?.name ?? "Unknown",
                                    awayTeam = competition.competitors[1]?.team?.name ?? "Unknown",
                                    homeTeamLogo = competition.competitors[0]?.team?.logo,
                                    awayTeamLogo = competition.competitors[1]?.team?.logo,
                                    homeTeamScore = competition.competitors[0]?.score ?? "0",
                                    awayTeamScore = competition.competitors[1]?.score ?? "0",
                                    //awayTeamOdds = competition.odds[0]?.awayTeamOdds.value,
                                    //homeTeamOdds = competition.odds[0]?.homeTeamOdds?.value,
                                    //drawOdds = competition.odds[0]?.drawOdds?.value,
                                    date = competition.date.Value,
                                    displayClock = eventData.competitions.Select(a => a.status.displayClock).Take(1).FirstOrDefault()
                                };
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Error processing match: {ex.Message}");
                                return null;
                            }
                        }).Where(match => match != null)
                    );
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating league games: {ex.Message}");
            }
        }
    }
}

