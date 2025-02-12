using SportsBook.Models;
using SportsBook.Service;
using Microsoft.Maui.Controls.Xaml;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml;
using System.Text.Json;
using SportsBook.ViewModels;


namespace SportsBook.Views
{

    public partial class GameStatsPage : ContentPage
    {
        private readonly GameStatsViewModel _viewModel;

        public GameStatsPage(EspnGameData espnGame) 
        {
            _viewModel = new GameStatsViewModel(espnGame);
            InitializeComponent();
            BindingContext = _viewModel;
            LoadData();

        }
        public async void LoadData()
        {
            await _viewModel.LoadGameStats();
        }
    }
}
