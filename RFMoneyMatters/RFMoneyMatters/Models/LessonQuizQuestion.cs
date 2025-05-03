namespace RFMoneyMatters.Models
{
    public class LessonQuizQuestion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string? C { get; set; }
        public string? D { get; set; }
        public string RightAnswer { get; set; }
        public string UserAnswer { get; set; }

        public int LessonQuizId { get; set; }
        public LessonQuiz LessonQuiz { get; set; }
    }
}