using GeoQuiz.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoQuiz.Models
{
    public class QuestionsList
    {
        private List<QuestionAnswerPair> questions;

        public int CorrectAnswersCount { get; private set; } = 0;
        public float CorrectAnswersPercent { get { return (float) CorrectAnswersCount / Count; } }

        public int WrongAnswersCount { get; private set; } = 0;
        public float WrongAnswersPercent { get { return (float) WrongAnswersCount / Count; } }

        public int LongestCorrectStreak { get; private set; } = 0;
        public int CurrentCorrectStreak { get; private set; } = 0;

        public int LongestWrongStreak { get; private set; } = 0;
        public int CurrentWrongStreak { get; private set; } = 0;

        public int CurrentQuestionIndex { get; private set; } = 0;

        public float PointsMultiplier { get; private set; } = 1.0f;
        private float maxPointsMultiplier = 10.0f;
        private int basicPointsForAnswer = 100;
        public int PointsForAnswer => (int) (basicPointsForAnswer * PointsMultiplier);
        public int Score { get; set; } = 0;

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
        public QuestionsList(List<QuestionAnswerPair> questions)
        {
            this.questions = questions;
            CurrentQuestionIndex = 0;
            CorrectAnswersCount = 0;
            WrongAnswersCount = 0;
        }

        public bool TestAnswer(string answer, bool goToNextQuestionAfter = true)
        {
            bool result = this[CurrentQuestionIndex].TestAnswer(answer);
            if (goToNextQuestionAfter)
            {
                CurrentQuestionIndex++;
                if (result)
                {
                    CorrectAnswersCount++;
                    CurrentCorrectStreak++;
                    if (CurrentCorrectStreak > LongestCorrectStreak)
                        LongestCorrectStreak = CurrentCorrectStreak;
                    CurrentWrongStreak = 0;

                    // Add points
                    Score += PointsForAnswer;
                    if (CurrentCorrectStreak > 1 && PointsMultiplier < maxPointsMultiplier)
                        PointsMultiplier += 0.1f;
                }
                else
                {
                    WrongAnswersCount++;
                    CurrentWrongStreak++;
                    if (CurrentWrongStreak > LongestWrongStreak)
                        LongestWrongStreak = CurrentWrongStreak;
                    CurrentCorrectStreak = 0;

                    PointsMultiplier = 1.0f;
                }
            }
            return result;
        }
    }
}