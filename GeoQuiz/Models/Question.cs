using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoQuiz.Models
{
    public class Question
    {
        public string QuestionString { get; private set; }
        public string[] Choices { get; private set; }

        public Question(string question, string[] choices)
        {
            QuestionString = question;
            Choices = choices;
        }
    }
}