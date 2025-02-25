﻿//using Kotlin.Reflect;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBook.Models
{
    public class EspnGameStatsData
    {
        public Boxscore? boxscore {  get; set; }
        public ObservableCollection<KeyEvents>? keyEvents { get; set; }
        public Header? header { get; set; }
    }
    
    public class Boxscore
    {
        //public List<Form> form { get; set; }
        public List<Teams>? teams { get; set; }

    }
    public class Teams
    {
        public Team? teams { get; set; }
        public ObservableCollection<GameStatistics>? statistics { get; set; }
        public int? displayOrder { get; set; }
        public string? homeAway { get; set; }
    }
    public class GameStatistics
    {
        public string? name { get; set; }
        public string? displayValue { get; set; }
        public string? label { get; set; }
    }
    public class KeyEvents
    {
        public string? id { get; set; }
        public Type? type { get; set; }
        public Clock? clock { get; set; }
        public string? text { get; set; }
        public string? shortText { get; set; }
        
    }
    public class Header
    {
        public LeagueInfo? league { get; set; }
    }
    public class LeagueInfo 
    { 
        public string? id { get; set; }
        public string? name { get; set; }
        public string? slug { get; set; }

    }
}
