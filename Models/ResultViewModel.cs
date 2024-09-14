namespace QuizTzy.Models
{
    public class ResultViewModel
    {
        public int CorrectCount { get; set; }
        public int TotalCount { get; set; }
        public WrongAnswer[] WrongAnswers { get; set; } = Array.Empty<WrongAnswer>();

        public class WrongAnswer
        {
            public Question Question { get; set; } = new Question();
            public int UserAnswer { get; set; }
        }
    }
}
