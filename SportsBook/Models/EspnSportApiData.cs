using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsBook.Models
{
    public class EspnSportApiData
    {

    }

    //step one sport names
    public class EspnSportsName
    {
        public string? SportName { get; set; }
        public string? SportUrl { get; set; }
        
    }

    //step two Sport info

    public class EspnSport
    {
        public string? Id { get; set; }
        public string? Guid { get; set; }
        public string? Uid { get; set; }
        public string? Name { get; set; }
        public string? DisplayName { get; set; }
        public string? Slug { get; set; }
        public EspnLeaguesRef? Leagues { get; set; }
        public EspnSportLeagueLogo[]? Logos { get; set; }
        public EspnSportLink[]? Links { get; set; }

        public List<EspnLeagueGroupNameAndRef>? LeagueNames { get; set; }
    }

    public class EspnLeagueGroupNameAndRef
    {
        public string? name { get; set; }
        public string? Url { get; set; }
        public string? SportSlug { get; set; }
        public string? LeagueSlug { get; set; }
    }
    public class EspnLeaguesRef
    {
        [JsonPropertyName("$ref")]
        public string? Ref { get; set; } // "$ref" is not a valid property name in C#, so use "Ref"
    }

    public class EspnSportLeagueLogo
    {
        public string? Href { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class EspnSportLink
    {
        public string? Href { get; set; }
        public string? Text { get; set; }
    }


    //step tre league names 
    public class EspnLeagueReference
    {
        [JsonPropertyName("$ref")]
        public string? Url { get; set; }
    }

    public class EspnLeagueListResponse
    {
        [JsonPropertyName("items")]
        public List<EspnLeagueReference>? Items { get; set; }
    }

    public class EspnLeagueDetails
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }


    public class EspnLeagueInfo
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("abbreviation")]
        public string? Abbreviation { get; set; }

        [JsonPropertyName("shortName")]
        public string? ShortName { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("country")]
        public EspnCountryDetails? Country { get; set; }

        [JsonPropertyName("season")]
        public EspnSeasonDetails? Season { get; set; }

        [JsonPropertyName("teams")]
        public EspnRefObject? Teams { get; set; }

        [JsonPropertyName("events")]
        public EspnRefObject? Events { get; set; }

        [JsonPropertyName("standings")]
        public EspnRefObject? Standings { get; set; }

        [JsonPropertyName("stats")]
        public EspnRefObject? Stats { get; set; }

        [JsonPropertyName("logos")]
        public List<EspnLeagueLogo>? Logos { get; set; }

        [JsonPropertyName("links")]
        public List<EspnLink>? Links { get; set; }
    }

    public class EspnCountryDetails
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("abbreviation")]
        public string? Abbreviation { get; set; }

        [JsonPropertyName("flag")]
        public EspnFlag? Flag { get; set; }
    }

    public class EspnFlag
    {
        [JsonPropertyName("href")]
        public string? ImageUrl { get; set; }
    }

    public class EspnSeasonDetails
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("startDate")]
        public string? StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public string? EndDate { get; set; }
    }

    public class EspnRefObject
    {
        [JsonPropertyName("$ref")]
        public string? RefUrl { get; set; }
    }

    public class EspnLeagueLogo
    {
        [JsonPropertyName("href")]
        public string? ImageUrl { get; set; }
    }

    public class EspnLink
    {
        [JsonPropertyName("href")]
        public string? Url { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }

}
