using SportsBook.Models;
using SportsBook.Service;
using SportsBook.Views;
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


        private ObservableCollection<EspnGameData>? _matchList;
        public ObservableCollection<EspnGameData>? MatchList
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








        //private CancellationTokenSource _cancellationTokenSource;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task FetchAndUpdateGames(EspnLeagueGroupNameAndRef selectedLeague, string leagueSlug)
        {
            try
            {
                //var leagueInfo = await _espnSportService.EspnFetchLeagueDetails(selectedLeague.Url);
                //matchInfo = await _espnSportService.EspnLeagueGamesData(selectedLeague.SportSlug, leagueInfo.Slug);
                var matchInfo = await _espnSportService.EspnLeagueGamesDataForMoreThenOneDay(selectedLeague.SportSlug, leagueSlug);
                if (MatchList == null)
                    MatchList = new ObservableCollection<EspnGameData>();
                else
                    MatchList.Clear(); // Clear old data

                foreach (var match in matchInfo)
                {
                    if (match.events != null && match.events.Any())
                    {
                        foreach (var eventData in match.events)
                        {
                            try
                            {
                                var competition = eventData.competitions.FirstOrDefault();
                                if (competition == null || competition.competitors.Count < 2)
                                    continue;

                                MatchList.Add(new EspnGameData
                                {
                                    gameId = eventData.id,
                                    gameLeague = leagueSlug,
                                    gameSport = selectedLeague.SportSlug,

                                    homeTeam = competition.competitors[0]?.team?.name ?? "Unknown",
                                    homeTeamLogo = competition.competitors[0]?.team?.logo,
                                    homeTeamScore = competition.competitors[0]?.score ?? "0",

                                    awayTeam = competition.competitors[1]?.team?.name ?? "Unknown",
                                    awayTeamLogo = competition.competitors[1]?.team?.logo,
                                    awayTeamScore = competition.competitors[1]?.score ?? "0",

                                    date = competition.date ?? DateTime.MinValue,
                                    
                                    gameStatus = eventData.competitions.Any(a => a.status.type.state == "post") ? "FT"
                                        : eventData.competitions.Any(a => a.status.type.state == "pre") ? $"{competition.date.Value:t}"
                                        : eventData.competitions.Select(a => a.status.displayClock).FirstOrDefault()
                                });
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Error processing match: {ex.Message}");
                            }
                        }

                        IsGamesVisible = MatchList.Any();
                        //StartPeriodicUpdate(selectedLeague, leagueInfo.Slug);
                    }
                    else
                    {
                        IsGamesVisible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching league games: {ex.Message}");
                // Display an error message
            }
        }

    }
}

