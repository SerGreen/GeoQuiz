using GeoQuiz.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoQuiz.Models
{
    public class GameSettings
    {
        public GameMode GameMode { get; set; } = GameMode.FlagByCountry;
        public Difficulty Difficulty { get; set; } = Difficulty.Medium;
        public int DistractorsAmount { get; set; } = 3;
        public int TimeLimit { get; set; } = 10;
        public List<int> AllowedNonSovereignIds { get; set; } = new List<int>();
        public List<string> Continents { get; set; } = new List<string>() { "NA", "SA", "EU", "AS", "AF", "AU" };
    }
}