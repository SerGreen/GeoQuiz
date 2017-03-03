using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoQuiz.Models.Shared
{
    public enum Difficulty { Easy = 100, Medium = 25, Hard = 10, VeryHard = 5 }
    public enum GameMode { FlagByCountry, CountryByFlag, CapitalByCountry }
}