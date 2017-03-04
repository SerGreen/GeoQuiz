using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoQuiz.Models
{
    public class QuestionsList
    {
        private List<QuestionAnswerPair> questions;

        public int CorrectCount { get { return _correctCount; } }
        public float CorrectPercent { get { return (float) _correctCount / TotalQuestionsCount; } }
        private int _correctCount = 0;

        public int WrongCount { get { return _wrongCount; } }
        public float WrongPercent { get { return (float) _wrongCount / TotalQuestionsCount; } }
        private int _wrongCount = 0;

        public int CurrentQuestionIndex { get { return _currentQuestionIndex; } }
        private int _currentQuestionIndex = 0;

        public QuestionAnswerPair this[int index]
        { get { return questions[index]; } }

        public int TotalQuestionsCount => questions.Count;

        public QuestionAnswerPair CurrentQuestion => EndReached ? null : this[_currentQuestionIndex];

        public bool EndReached { get { return _currentQuestionIndex >= TotalQuestionsCount; } }

        /// <summary>
        /// This will create empty list of questions. You should not use this constructor.
        /// </summary>
        public QuestionsList() : this(new List<QuestionAnswerPair>())
        { }

        /// <summary>
        /// This constructor should be used to create list.
        /// </summary>
        public QuestionsList(List<QuestionAnswerPair> questions)
        {
            this.questions = questions;
            _currentQuestionIndex = 0;
            _correctCount = 0;
            _wrongCount = 0;
        }

        public bool TestAnswer(string answer, bool goToNextQuestionAfter = true)
        {
            bool result = CurrentQuestion.TestAnswer(answer);
            if (goToNextQuestionAfter)
                _currentQuestionIndex++;
            return result;
        }
    }
}