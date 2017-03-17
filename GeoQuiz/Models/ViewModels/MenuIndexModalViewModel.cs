using GeoQuiz.Database.DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoQuiz.Models
{
    public class MenuIndexModalViewModel
    {
        public List<int> AllowedNonSovereignIds { get; set; }
        public IEnumerable<Country> AllCountries { get; set; }
    }
}