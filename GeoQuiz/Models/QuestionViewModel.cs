namespace GeoQuiz.Models
{
    public class QuestionViewModel
    {
        public int Index { get; set; }
        public int TotalQuestionsCount { get; set; }
        public Question Question { get; set; }
    }
}