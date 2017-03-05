using GeoQuiz.Models.Shared;

namespace GeoQuiz.Models
{
    public class QuestionViewModel
    {
        public int Index { get; set; }
        public int TotalQuestionsCount { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public Question Question { get; set; }
        public GameMode GameMode { get; set; }
    }
}