using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsBook.Models
{
    public class EspnSportLeagueData
    {
        public IEnumerable<Events>? events { get; set; }

    }

   

    public class Events
    {
        [JsonPropertyName("id")]
        public string? id { get; set; }
        [JsonPropertyName("uid")]
        public string? uid { get; set; }
        [JsonPropertyName("date")]
        public DateTime? date { get; set; }
        public string? name { get; set; }
        public string? shortName { get; set; }
        public Season? season { get; set; }
        public List<Competitions>? competitions { get; set; }
        public Status? status { get; set; }
        public Venue? venue { get; set; }
        //public List<Links> links { get; set; }
    }
    public class Season
    {
        public int? year { get; set; }
        public int? type { get; set; }
        public string? slug { get; set; }
    }
    public class Competitions
    {
        public string? id { get; set; }
        public string? uid { set; get; }
        public DateTime? date { get; set; }
        public DateTime? startDate { get; set; }
        public int? attendance { get; set; }
        public bool? timeValid { get; set; }
        public bool? recent { get; set; }
        public Status? status { get; set; }
        public Venue? venue { get; set; }
        public Format? format { get; set; }
        //public string[]? notes { get; set; }
        //public List<GeoBroadcasts> geoBroadcasts { get; set; }
        //public List<Broadcasts> broadcasts { get; set; }
        //public string broadcast { get; set; }
        public List<Competitors>? competitors { get; set; }
        public List<Details>? details { get; set; }
        public List<Headlines>? headlines { get; set; }
        public List<Odds>? odds { get; set; }
        public bool wasSuspended { get; set; }
        public bool playByPlayAvailable { get; set; }
        public bool playByPlayAthletes { get; set; }
    }
    public class Odds
    {
        public Provider? provider { get; set; }
        public AwayTeamOdds? awayTeamOdds { get; set; }
        public HomeTeamOdds? homeTeamOdds { get; set; }
        public DrawOdds? drawOdds { get; set; }

    }
    public class Provider
    {
        public string? id { set; get; }
        public string? name { set; get; }
        public int priority { set; get; }
    }
    public class AwayTeamOdds
    {
        public double value { get; set; }

    }
    public class HomeTeamOdds
    {
        public double value {  get; set; }
    }
    public class DrawOdds
    {
        public double value { get; set; }
    }

    public class Status
    {
        public double? clock { get; set; }
        public string? displayClock { get; set; }
        public int? period { get; set; }
        public Type? type { get; set; }
    }
    public class Venue
    { 
        public string? id {  set; get; }
        public string? fullName { set; get; }
        public Address? address { set; get; }
        public string? displayName { set; get; }
    }
    public class Format 
    { 
        public Regulation? regulation { set; get; }
    }
    public class Type 
    {
        public string? id {  set; get; }
        public string? name { set; get; }
        public string? state { set; get; }
        public bool completed { get; set; }
        public string? description { get; set; }
        public string? detail { get; set; }
        public string? shortDetail { get; set; }
        public string? text { set; get; }
    }

    public class Address
    {
        public string? city { set; get; }
        public string? country { set; get; }
    }
    public class Regulation 
    { 
        public byte periods { get; set; } 
    }

    public class Competitors
    {
        public string? id { set; get; }
        public string? uid { set; get; }
        public string? type { set; get; }
        public byte? order { set; get; }
        public string? homeAway { set; get; }
        public bool? winner { set; get; }
        public string? from { set; get; }
        public string? score { set; get; }
        public List<Records>? records { set; get; }
        public Team? team { set; get; }
        public List<Statistics>? statistics { set; get; }
    }

    public class Records
    {
        public string? name { get; set; }
        public string? type { get; set; }
        public string? summary { set; get; }
        public string? abbreviation { set; get; }
    }
    public class Team
    {
        public string? id { set; get; }
        public string? uid {  get; set; }
        public string? abbreviation {  get; set; }
        public string? displayName { set; get; }
        public string? shortDisplayName { set; get; }
        public string? name { get; set; }
        public string? location { set; get; }
        public string? color { set; get; }
        public string? alternateColor { set; get; }
        public bool isActive { set; get; }
        public string? logo { set; get; }
        //public List<Links> links { set; get; }
        public Venue? venue { set; get; }
    }

    public class Statistics
    {
        public string? name { set; get; }
        public string? abbreviation { get; set; }
        public string? displayValue { set; get; }

    }

    public class Details
    {
        public Type? type { set; get; }
        public Clock? clock { set; get; }
        public Team? team { set; get; }

        public double scoreValue { set; get; }
        public bool scoringPlay { set; get; } 
        public bool redCard { get; set; }
        public bool yellowCard { get; set; }
        public bool penaltyKick { set; get; }
        public bool ownGoal { set; get; }
        public bool shootout { get; set; }
        public List<AthletesInvolved>? athletesInvolved { set; get; }

    }
    public class Clock
    {
        public double value { set; get; }
        public string? displayValue { set; get; }
    }
    public class AthletesInvolved
    {
        public string? id { set; get; }
        public string? displayName { set; get; }
        public string? shortName { set; get; }
        public string? fullName { set; get; }
        public string? jersey { set; get; }
        public Team? team { get; set; }
        //public List<Links> links { set; get; }
        public string? position { set; get; }
    }
    public class Headlines
    {
        public string? description { set; get; }
        public string? type { set; get; }
        public string? shortLinkText { set; get; }
    }


}
