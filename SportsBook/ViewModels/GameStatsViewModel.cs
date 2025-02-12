using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsBook.Models;
using SportsBook.Service;

namespace SportsBook.ViewModels
{
    internal class GameStatsViewModel : INotifyPropertyChanged
    {
        private readonly EspnSportService _espnSportService;
        private readonly EspnGameData _espnGameData;
        public event PropertyChangedEventHandler? PropertyChanged;


        public GameStatsViewModel(EspnGameData espnGameData) 
        {
            _espnSportService = new EspnSportService();
            _espnGameData = espnGameData;
        }


        public async Task LoadGameStats()
        {
            var Gamestats = await _espnSportService.GetGameStats(_espnGameData.gameId, _espnGameData.gameSport, _espnGameData.gameLeague);
        }
    }
}
