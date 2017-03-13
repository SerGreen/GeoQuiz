using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoQuiz.Models.Shared
{
    public enum Difficulty { Easy = 100, Medium = 25, Hard = 12, VeryHard = 7 }
    public enum GameMode { FlagByCountry, CountryByFlag, CapitalByCountry }
}