using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SportsBook.Models;
using SportsBook.Service;

namespace SportsBook.ViewModels
{
    internal class GameStatsViewModel : INotifyPropertyChanged
    {
        private readonly EspnSportService _espnSportService;
        private EspnGameData _espnGameData;
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<KeyEvents> _keyEvents;

        public ObservableCollection<KeyEvents> KeyEvents
        {
            get => _keyEvents;
            set
            {
                _keyEvents = value;
                OnPropertyChanged(nameof(KeyEvents));
            }
        }

        public EspnGameStats HomeTeamStats { get; set; } = new EspnGameStats();
        public EspnGameStats AwayTeamStats { get; set; } = new EspnGameStats();
        public EspnGameData EspnGameData
        {
            get => _espnGameData;
            set
            {
                _espnGameData = value;
                OnPropertyChanged(nameof(EspnGameData));
            }
        }

        public GameStatsViewModel(EspnGameData espnGameData)
        {
            _espnSportService = new EspnSportService();
            EspnGameData = espnGameData;
            KeyEvents = new ObservableCollection<KeyEvents>();
        }
        


        public async Task LoadGameStats()
        {
            try
            {
                var gameStatsResponse = await _espnSportService.GetGameStats(
                    _espnGameData.gameId,
                    _espnGameData.gameSport,
                    _espnGameData.gameLeague
                );

                if (gameStatsResponse?.boxscore?.teams != null && gameStatsResponse.boxscore.teams.Count >= 2)
                {
                    var homeTeam = gameStatsResponse.boxscore.teams[0];
                    var awayTeam = gameStatsResponse.boxscore.teams[1];
                    KeyEvents = gameStatsResponse.keyEvents;

                    // Extract statistics for Home Team
                    HomeTeamStats.Fouls = homeTeam.statistics.FirstOrDefault(s => s.name == "foulsCommitted")?.displayValue ?? "0";
                    HomeTeamStats.Shots = homeTeam.statistics.FirstOrDefault(s => s.name == "totalShots")?.displayValue ?? "0";
                    HomeTeamStats.ShotsOnTarget = homeTeam.statistics.FirstOrDefault(s => s.name == "shotsOnTarget")?.displayValue ?? "0";
                    HomeTeamStats.Corners = homeTeam.statistics.FirstOrDefault(s => s.name == "wonCorners")?.displayValue ?? "0";
                    HomeTeamStats.RedCards = homeTeam.statistics.FirstOrDefault(s => s.name == "redCards")?.displayValue ?? "0";
                    HomeTeamStats.YellowCards = homeTeam.statistics.FirstOrDefault(s => s.name == "yellowCards")?.displayValue ?? "0";
                    HomeTeamStats.PossessionPct = homeTeam.statistics.FirstOrDefault(s => s.name == "possessionPct")?.displayValue ?? "0";
                    HomeTeamStats.Offsides = homeTeam.statistics.FirstOrDefault(s => s.name == "offsides")?.displayValue ?? "0";
                    HomeTeamStats.Passes = homeTeam.statistics.FirstOrDefault(s => s.name == "totalPasses")?.displayValue ?? "0";
                   

                    // Extract statistics for Away Team
                    AwayTeamStats.Fouls = awayTeam.statistics.FirstOrDefault(s => s.name == "foulsCommitted")?.displayValue ?? "0";
                    AwayTeamStats.Shots = awayTeam.statistics.FirstOrDefault(s => s.name == "totalShots")?.displayValue ?? "0";
                    AwayTeamStats.ShotsOnTarget = awayTeam.statistics.FirstOrDefault(s => s.name == "shotsOnTarget")?.displayValue ?? "0";
                    AwayTeamStats.Corners = awayTeam.statistics.FirstOrDefault(s => s.name == "wonCorners")?.displayValue ?? "0";
                    AwayTeamStats.RedCards = awayTeam.statistics.FirstOrDefault(s => s.name == "redCards")?.displayValue ?? "0";
                    AwayTeamStats.YellowCards = awayTeam.statistics.FirstOrDefault(s => s.name == "yellowCards")?.displayValue ?? "0";
                    AwayTeamStats.PossessionPct = awayTeam.statistics.FirstOrDefault(s => s.name == "possessionPct")?.displayValue ?? "0";
                    AwayTeamStats.Offsides = awayTeam.statistics.FirstOrDefault(s => s.name == "offsides")?.displayValue ?? "0";
                    AwayTeamStats.Passes = awayTeam.statistics.FirstOrDefault(s => s.name == "totalPasses")?.displayValue ?? "0";

                    // Notify UI of property changes
                    OnPropertyChanged(nameof(HomeTeamStats));
                    OnPropertyChanged(nameof(AwayTeamStats));
                    OnPropertyChanged(nameof(KeyEvents));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game stats: {ex.Message}");
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
