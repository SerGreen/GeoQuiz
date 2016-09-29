using GeoQuiz.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoQuiz.Models
{
    public class QuestionAnswerPair
    {
        public Question Question { get; private set; }
        public int CorrectAnswerIndex { get; private set; }
        public string Answer { get { return Question.Choices[CorrectAnswerIndex]; } }

        public QuestionAnswerPair(string question, string answer, string[] distractors)
        {
            List<string> choices = new List<string>(distractors);
            choices.Add(answer);
            choices = choices.Shuffle().ToList();

            CorrectAnswerIndex = choices.IndexOf(answer);
            Question = new Question(question, choices.ToArray());
        }
    }
}