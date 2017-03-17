using GeoQuiz.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace GeoQuiz.Models
{
    public class QuestionAnswerPair
    {
        public Question Question { get; private set; }
        private int CorrectAnswerIndex { get; }
        public string Answer { get { return Question.Choices[CorrectAnswerIndex]; } }
        public string AnswerAlias { get; }

        public QuestionAnswerPair(string question, string answer, string[] distractors, string answerAlias = null)
        {
            List<string> choices = new List<string>(distractors);
            choices.Add(answer);
            choices = choices.Shuffle().ToList();

            CorrectAnswerIndex = choices.IndexOf(answer);
            AnswerAlias = answerAlias != null ? Regex.Replace(answerAlias, @"[\[|\]|'|’]", " ") : null;
            Question = new Question(question, choices.ToArray());
        }

        public bool TestAnswer(string answer)
        {
            string playerAnswer = Regex.Replace(answer, @"[\[|\]|'|’]", " ");
            string correctAnswer = Regex.Replace(answer, @"[\[|\]|'|’]", " ");
            return playerAnswer.Equals(correctAnswer, StringComparison.InvariantCultureIgnoreCase) 
                || (AnswerAlias != null && playerAnswer.Equals(AnswerAlias, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}