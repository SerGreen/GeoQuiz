using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoQuiz.Models
{
    public class ResultsViewModel
    {
        public QuestionsList Questions { get; set; }
        public GameSettings GameSettings { get; set; }
    }
}