using GeoQuiz.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoQuiz.Models
{
    public class QuestionsList
    {
        public GameMode GameMode { get; }

        private List<QuestionAnswerPair> questions;

        public int CorrectAnswersCount { get { return _correctAnswersCount; } }
        public float CorrectAnswersPercent { get { return (float) _correctAnswersCount / Count; } }
        private int _correctAnswersCount = 0;

        public int WrongAnswersCount { get { return _wrongAnswersCount; } }
        public float WrongAnswersPercent { get { return (float) _wrongAnswersCount / Count; } }
        private int _wrongAnswersCount = 0;

        public int CurrentQuestionIndex { get { return _currentQuestionIndex; } }
        private int _currentQuestionIndex = 0;

        public QuestionAnswerPair this[int index]
        { get { return index < Count ? questions[index] : null; } }

        public int Count => questions.Count;

        public Question CurrentQuestion => EndReached ? null : this[CurrentQuestionIndex].Question;

        public bool EndReached { get { return CurrentQuestionIndex >= Count; } }

        /// <summary>
        /// This will create empty list of questions. You should not use this constructor.
        /// </summary>
        public QuestionsList() : this(new List<QuestionAnswerPair>())
        { }

        /// <summary>
        /// This constructor should be used to create list.
        /// </summary>
        public QuestionsList(List<QuestionAnswerPair> questions, GameMode gameMode = GameMode.FlagByCountry)
        {
            this.questions = questions;
            _currentQuestionIndex = 0;
            _correctAnswersCount = 0;
            _wrongAnswersCount = 0;
            GameMode = gameMode;
        }

        public bool TestAnswer(string answer, bool goToNextQuestionAfter = true)
        {
            bool result = this[CurrentQuestionIndex].TestAnswer(answer);
            if (goToNextQuestionAfter)
            {
                _currentQuestionIndex++;
                if (result)
                    _correctAnswersCount++;
                else
                    _wrongAnswersCount++;
            }
            return result;
        }
    }
}