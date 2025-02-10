using SportsBook.Models;
using SportsBook.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SportsBook.ViewModels
{
    public class SportsPageViewModel : INotifyPropertyChanged
    {
        private readonly EspnSportService _espnSportService;
        private ObservableCollection<EspnLeagueGroupNameAndRef>? _espnLeagueList;
        public event PropertyChangedEventHandler? PropertyChanged;

        private GameViewModel _gameViewModel;
        public GameViewModel GameViewModel
        {
            get => _gameViewModel;
            set
            {
                if (_gameViewModel != value)
                {
                    _gameViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
        


        public ObservableCollection<EspnLeagueGroupNameAndRef>? EspnLeagueList
        {
            get => _espnLeagueList;
            set
            {
                _espnLeagueList = value;
                OnPropertyChanged();
            }
        }
        public SportsPageViewModel(EspnSportService espnSportService)
        {
            _espnSportService = espnSportService;
            EspnLeagueList = new ObservableCollection<EspnLeagueGroupNameAndRef>();
            GameViewModel = new GameViewModel();
        }

        public async Task LoadLeagueGamesAndScoresDataAsync(string sportUrl)
        {
            var leagues = await _espnSportService.EspnFetchSportData(sportUrl);
            EspnLeagueList.Clear();
            foreach (var league in leagues.LeagueNames)
            {
                EspnLeagueList.Add(league);
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
